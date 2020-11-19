using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<ApplicationRole, RoleDto>();
        }
    }
}
