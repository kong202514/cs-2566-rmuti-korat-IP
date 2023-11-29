using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseApiController
{
    private DataContext _dataContext;
    private readonly ITokenService _tokenService;

    public AccountController(DataContext dataContext, ITokenService tokenService)
    {
        _dataContext = dataContext;
        _tokenService = tokenService;
    }
    private async Task<bool> isUserExists(string username)
    {
        return await _dataContext.Users.AnyAsync(user => user.UserName == username.ToLower());
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)

    {



        if (await isUserExists(registerDto.UserName!))
            return BadRequest("username is already exists");




        // / ใช้ using เพื่อไม่ให้ garbage collector จัดการ
        using var hmacSHA256 = new HMACSHA256(); //HMACSHA256 inherit มาจาก IDisposable interface

        var user = new AppUser
        {
            UserName = registerDto.UserName.Trim().ToLower(),
            PasswordHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password.Trim())),
            PasswordSalt = hmacSHA256.Key
        };

        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return new UserDto
        {
            UserName = user.UserName,
            token = _tokenService.CreateToken(user)
        };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(user =>
                            user.UserName == loginDto.UserName);

        if (user is null) return Unauthorized("invalid username");

        using var hmacSHA256 = new HMACSHA256(user.PasswordSalt!);

        var computedHash = hmacSHA256.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password!.Trim()));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash?[i]) return Unauthorized("invalid password");
        }
        return new UserDto
        {
            UserName = user.UserName,
            token = _tokenService.CreateToken(user)
        }; ;
    }
}