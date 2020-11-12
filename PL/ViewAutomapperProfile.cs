using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL.Models;
using PL.Models;

namespace PL
{
    public class ViewAutomapperProfile : Profile
    {
        public ViewAutomapperProfile()
        {
            CreateMap<UserViewModel, UserModel>().ReverseMap();
        }
    }
}