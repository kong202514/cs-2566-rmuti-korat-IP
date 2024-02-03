using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public UsersController(
            IUserRepository userRepository,
            IMapper mapper,
            IImageService imageService
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        private async Task<AppUser?> _GetUser()
        {
            string? username = User.GetUsername();
            return username is null ? null : await _userRepository.GetUserByUserNameAsync(username);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PageList<MemberDto>>> GetUsers(
            [FromQuery] UserParams userParams
        )
        {
            string? username = User.GetUsername();
            if (username is null)
            {
                return NotFound();
            }

            AppUser? currentUser = await _userRepository.GetUserByUserNameAsync(username);
            if (currentUser is null)
            {
                return NotFound();
            }

            userParams.CurrentUserName = currentUser.UserName;
            if (string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender =
                    currentUser.Gender != "non-binary"
                        ? currentUser.Gender == "male"
                            ? "female"
                            : "male"
                        : "non-binary";
            }
            PageList<MemberDto> pages = await _userRepository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(
                new PaginationHeader(
                    pages.CurrentPage,
                    pages.PageSize,
                    pages.TotalCount,
                    pages.TotalPages
                )
            );
            return Ok(pages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberDto?>> GetUser(int id)
        {
            AppUser? user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<MemberDto>(user);
        }

        [HttpGet("username/{username}")]
        public async Task<ActionResult<MemberDto?>> GetUserByUsername(string username)
        {
            return await _userRepository.GetMemberByUserNameAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserProfile(MemberUpdateDto memberUpdateDto)
        {
            AppUser? appUser = await _GetUser();

            if (appUser is null)
            {
                return NotFound();
            }

            _ = _mapper.Map(memberUpdateDto, appUser);
            return await _userRepository.SaveAllAsync()
                ? NoContent()
                : BadRequest("Failed to update user profile!");
        }

        [HttpPost("add-image")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            AppUser? user = await _GetUser();
            if (user is null)
            {
                return NotFound();
            }

            CloudinaryDotNet.Actions.ImageUploadResult result = await _imageService.AddImageAsync(
                file
            );
            if (result.Error is not null)
            {
                return BadRequest(result.Error.Message);
            }

            Photo photo = new() { Url = result.SecureUrl.AbsoluteUri, PublicId = result.PublicId };
            if (user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            return await _userRepository.SaveAllAsync()
                ? (ActionResult<PhotoDto>)
                    CreatedAtAction( //status 201
                        nameof(GetUserByUsername),
                        new { username = user.UserName },
                        _mapper.Map<PhotoDto>(photo)
                    )
                : (ActionResult<PhotoDto>)BadRequest("Something has gone wrong!");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId)
        {
            AppUser? user = await _GetUser();
            if (user is null)
            {
                return BadRequest("User is required");
            }

            Photo? photo = user.Photos.FirstOrDefault(photo => photo.Id == photoId);
            if (photo is null)
            {
                return BadRequest("Photo is required");
            }

            if (photo.IsMain)
            {
                return BadRequest("this photo(id:" + photo.Id + ") is already main photo");
            }

            Photo? currentMainPhoto = user.Photos.FirstOrDefault(photo => photo.IsMain);
            if (currentMainPhoto is not null)
            {
                currentMainPhoto.IsMain = false;
            }

            photo.IsMain = true;

            return await _userRepository.SaveAllAsync()
                ? NoContent()
                : BadRequest("Something has gone wrong!");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            AppUser? user = await _GetUser();
            if (user is null)
            {
                return NotFound();
            }

            Photo? photo = user.Photos.FirstOrDefault(photo => photo.Id == photoId);
            if (photo is null)
            {
                return NotFound();
            }

            if (photo.IsMain)
            {
                return BadRequest("can't delete main photo");
            }

            if (photo.PublicId is not null)
            {
                CloudinaryDotNet.Actions.DeletionResult result =
                    await _imageService.DeleteImageAsync(photo.PublicId);
                if (result.Error is not null)
                {
                    return BadRequest(result.Error.Message);
                }
            }

            _ = user.Photos.Remove(photo);
            return await _userRepository.SaveAllAsync()
                ? NoContent()
                : BadRequest("Something has gone wrong!");
        }
    }
}
