using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL.Models;
using DAL.Entities;
using PL.Models;

namespace PL
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            //CreateMap<CustomUser, UserModel>().ForMember(dest => dest.UserModelEmail, opt => opt.MapFrom(src => src.UserEmail));

            //CreateMap<UserModel, UserViewModel>()
            //    .ForMember(dest => dest.UserViewModelEmail, opt => opt.MapFrom(src => src.UserModelEmail))
            //    .ForMember(dest => dest.UserViewModelId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.UserViewModelImagePath, opt => opt.MapFrom(src => src.UserImagePath))
            //    .ForMember(dest => dest.UserViewModelName, opt => opt.MapFrom(src => src.UserName)).ReverseMap();
        }
    }
}