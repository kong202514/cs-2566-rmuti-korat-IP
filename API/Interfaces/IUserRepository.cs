using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);




    Task<bool> SaveAllAsync();



    Task<AppUser?> GetUserByIdAsync(int id);




    Task<AppUser?> GetUserByUserNameAsync(string username);



    Task<AppUser?> GetUserByUserNameWithOutPhotoAsync(string username);





    Task<MemberDto> GetMemberbyUserNameAsync(UserParams userParams);
    Task<MemberDto?> GetMembersAsync(string username);
    Task GetMembersAsync(UserParams userParams);
    Task<ActionResult<MemberDto?>> GetMemberAsync(string username);
}

