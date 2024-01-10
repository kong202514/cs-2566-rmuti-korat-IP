using System;
using API.Entities;
using API.Extensions;
using AutoMapper;
using Company.ClassLibrary1;

namespace API.Helpers;

public class AutoMapperUserProfiles : Profile
{
    public AutoMapperUserProfiles()
    {
        CreateMap<AppUser, MemberDto>();
        CreateMap<Photo, PhotoDto>();


        CreateMap<AppUser, MemberDto>()
                 .ForMember(
                      user => user.Age,
                      opt => opt.MapFrom(user => user.BirthDate.CalculateAge())
                  )
                .ForMember(
                    user => user.Age,
                    opt => opt.MapFrom(user => user.BirthDate.CalculateAge())
                );
        CreateMap<Photo, PhotoDto>();



    }



}
