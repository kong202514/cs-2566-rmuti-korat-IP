namespace API.Controllers;

using API.Data;
using API.Entities;
using AutoMapper;
using Company.ClassLibrary1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetUsersAsync();
        return Ok(_mapper.Map<IEnumerable<MemberDto>>(users));
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








