using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private DataContext _dataContext;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(IMapper mapper, DataContext dataContext, ITokenService tokenService)
    {
        _mapper = mapper;
        _dataContext = dataContext;
        _tokenService = tokenService;
    }
    private async Task<bool> isUserExists(string username) => await _dataContext.Users.AnyAsync(user => user.UserName == username.ToLower());



    var user = await _dataContext.Users
                      .Include(photo => photo.Photos)
                      .SingleOrDefaultAsync(user =>
                          user.UserName == loginDto.UserName);

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)

    {



        if (await isUserExists(registerDto.UserName!))
            return BadRequest("username is already exists");
        var user = _mapper.Map<AppUser>(registerDto);



        // / ใช้ using เพื่อไม่ให้ garbage collector จัดการ
        // using var hmacSHA256 = new HMACSHA256(); //HMACSHA256 inherit มาจาก IDisposable interface

        // var user = new AppUser
        // {
        //     UserName = registerDto.UserName.Trim().ToLower(),
        //     PasswordHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password.Trim())),
        //     PasswordSalt = hmacSHA256.Key
        // };

        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            token = _tokenService.CreateToken(user),
            Aka = user.Aka,
            Gender = user.Gender
        };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(user =>
                            user.UserName == loginDto.UserName);

        if (user is null) return Unauthorized("invalid username");

        // using var hmacSHA256 = new HMACSHA256(user.PasswordSalt!);
        // var computedHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password!.Trim()));
        // for (int i = 0; i < computedHash.Length; i++)
        // {
        // if (computedHash[i] != user.PasswordHash?[i]) return Unauthorized("invalid password");
        // }
        return new UserDto
        {
            Username = user.UserName,
            token = _tokenService.CreateToken(user),
            Aka = user.Aka,
            Gender = user.Gender,
            PhotoUrl = user.Photos.FirstOrDefault(photo => photo.IsMain)?.Url
        };
    }
}