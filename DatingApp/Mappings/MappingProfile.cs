using AutoMapper;
using DatingApp.DTOS;
using DatingApp.Helpers;
using DatingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Migrations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<UserApplication, UserListDTO>()
                .ForMember(dest => dest.MainPhotoUrl, op => op.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(det => det.Age, op => op.MapFrom(src => src.DateOfBirth.Age()));
            CreateMap<UserApplication, UserDetailsDTO>()
               .ForMember(dest => dest.MainPhotoUrl, op => op.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
              .ForMember(det => det.Age, op => op.MapFrom(src => src.DateOfBirth.Age()));
            CreateMap<UserPhoto, UserPhotoDTO>();
            CreateMap<UserForUpdateDTO, UserApplication>();
            CreateMap<UserPhoto, PhotoForReturnDTO>();
            CreateMap<PhotoForCreationDTO, UserPhoto>();
        }
    
    }
}
