using System;
using API.Entities;

namespace Company.ClassLibrary1;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    // Task<AppUser?> GetUserByIdAsync(int id);
    // Task<AppUser?> GetUserByUserNameAsync(string username);
    Task<IEnumerable<AppUser>> GetUsersAsync();

    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto?> GetMemberAsync(string username);
    Task GetUserByIdAsync(int id);
}

