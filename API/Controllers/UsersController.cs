

using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
// [Authorize]

[ApiController]
[Route("api/[controller]")] // [controller] = Users, (UsersController - Controller = User), ~route = /api/users
public class UsersController : ControllerBase
{
    private readonly DataContext _dataContext;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.



    public UsersController(IUserRepository userRepository, IMapper mapper)

    {


        this._userRepository = userRepository;
        this._mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]

    public async Task<ActionResult<PageList<MemberDto>>> GetUsers([FromQuery] UserParams userParams)
    {


        var username = User.GetUsername();
        if (username is null) return NotFound();

        var currentUser = await _userRepository.GetUserByUserNameAsync(username);
        if (currentUser is null) return NotFound();
        userParams.CurrentUserName = currentUser.UserName;
        if (string.IsNullOrEmpty(userParams.Gender))
        {
            if (currentUser.Gender != "non-binary")
                userParams.Gender = currentUser.Gender == "male" ? "female" : "male";
            else
                userParams.Gender = "non-binary";
        }
        var pages = await _userRepository.GetMembersAsync(userParams);
        Response.AddPaginationHeader(
            new PaginationHeader(pages.CurrentPage, pages.PageSize, pages.TotalCount, pages.TotalPages));
        return Ok(pages);
    }


    public async Task<ActionResult<MemberDto?>> GetUser(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return _mapper.Map<MemberDto>(user);
    }



    [HttpGet("{id}")] //endpoint: /api/users/25  
    public async Task<ActionResult<AppUser?>> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }




    [HttpGet("username/{username}")]


    public async Task<ActionResult<MemberDto?>> GetUserByUserName(string username)
    {
        // var user = await _userRepository.GetUserByUserNameAsync(username);
        // return _mapper.Map<MemberDto>(user);
        return await _userRepository.GetMemberAsync(username);
    }
}








