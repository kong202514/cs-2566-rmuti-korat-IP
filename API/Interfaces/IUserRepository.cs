using System;
using API.Entities;
using API.Extensions;
using API.Helpers;

namespace Company.ClassLibrary1;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUserNameAsync(string username);
    Task<IEnumerable<AppUser>> GetUsersAsync();
    //Task<IEnumerable<MemberDto>> GetMembersAsync();
    // Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams);
    Task<MemberDto?> GetMemberAsync(string username);

}

