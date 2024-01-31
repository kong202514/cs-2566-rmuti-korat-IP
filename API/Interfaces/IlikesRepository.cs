﻿using System;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;
public interface IlikesRepository
{
    Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);

    Task<AppUser> GetUser(int userId);


    Task<PageList<LikeDto>> GetUserLikes(LikesParams likesParams);
    Task GetUserLikes(object likesParams);
}
