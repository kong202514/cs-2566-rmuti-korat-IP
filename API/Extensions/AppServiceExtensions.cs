﻿using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;

using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class AppServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration conf)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(conf.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IlikesRepository, LikesRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<LogUserActivity>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
          services.Configure<CloudinarySettings>(conf.GetSection("CloudinarySettings"));
       services.AddScoped<IImageService, ImageService>();
 

        return services;
    }
}