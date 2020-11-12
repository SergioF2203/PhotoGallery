using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;

namespace PL.Util
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();

            Bind<IMapper>().ToMethod(ctx =>
            {
                return new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ViewAutomapperProfile>();
                }).CreateMapper();
            }).InSingletonScope();
        }
    }
}