using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;


namespace API.Helpers;

public class AutoMapperUserProfiles : Profile
{
    public AutoMapperUserProfiles()
    {
        CreateMap<AppUser, MemberDto>();
        CreateMap<Photo, PhotoDto>();




        CreateMap<Message, MessageDto>()
                   .ForMember(
                       ms_dto => ms_dto.SenderPhotoUrl,
                       opt => opt.MapFrom(
                               ms => ms.Sender.Photos.FirstOrDefault(photo => photo.IsMain).Url
                           )
                   )
                   .ForMember(
                       ms_dto => ms_dto.RecipientPhotoUrl,
                       opt => opt.MapFrom(
                               ms => ms.Recipient.Photos.FirstOrDefault(photo => photo.IsMain).Url
                           )
                   );
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

        CreateMap<RegisterDto, AppUser>();



    }



}
