using System;
using API.Entities;
using API.Extensions;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Company.ClassLibrary1;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        return await _dataContext.Users
             .Where(user => user.UserName == username)
             // .Select(user => new MemberDto
             // {
             //     Id = user.Id,
             //     UserName = user.UserName,
             //      ...
             // })
             .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
             .SingleOrDefaultAsync();
    }

    //public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    public async Task<PageList<MemberDto>> GetMembersAsync(UserParams userParams)
    {


        var minBirthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MaxAge - 1));
        var maxBirthDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-userParams.MinAge));






        var query = _dataContext.Users.AsQueryable();
        query = query.Where(user => user.BirthDate >= minBirthDate && user.BirthDate <= maxBirthDate);

        query = query.Where(user => user.UserName != userParams.CurrentUserName);
        if (userParams.Gender != "non-binary")
            query = query.Where(user => user.Gender == userParams.Gender);
        query.AsNoTracking();
        return await PageList<MemberDto>.CreateAsync(
            query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
            userParams.PageNumber,
            userParams.PageSize);
    }

    public async Task<MemberDto?> GetUserByIdAsync(int id)
    {
        return await _dataContext.Users
           .Where(user => user.Id == id)
           .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync();
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string username)
    {
        return await _dataContext.Users
        .Include(user => user.Photos)
        .SingleOrDefaultAsync(user => user.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _dataContext.Users
        .Include(user => user.Photos)
        .ToListAsync();
    }
    public async Task<bool> SaveAllAsync() => await _dataContext.SaveChangesAsync() > 0;

    public void Update(AppUser user) => _dataContext.Entry(user).State = EntityState.Modified;

    Task<AppUser?> IUserRepository.GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}

internal class _mapper
{
    public static AutoMapper.IConfigurationProvider ConfigurationProvider { get; internal set; }
}