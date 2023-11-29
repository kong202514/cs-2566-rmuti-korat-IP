namespace API.Controllers;

using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// [Authorize]

[ApiController]
[Route("api/[controller]")] // [controller] = Users, (UsersController - Controller = User), ~route = /api/users
public class UsersController : ControllerBase
{
    private readonly DataContext _dataContext;

    public UsersController(DataContext dataContext)
    {
        //putting cursor inside dataContext (ctor parameter) `ctrl+.` then select `create and assign feild`
        this._dataContext = dataContext;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Entities.AppUser>>> GetUsers()
    {
        return await _dataContext.Users.ToListAsync();
    }
    [HttpGet("{id}")] //endpoint: /api/users/25 ,when id = 25
    public async Task<ActionResult<AppUser?>> GetUser(int id)
    {
        return await _dataContext.Users.FindAsync(id);
    }

}