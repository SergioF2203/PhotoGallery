using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace PL.Util
{
    public class UserModule : NinjectModule
    {
        //private readonly string _connectionString;

        //public UserModule() { }

        //public UserModule(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public override void Load()
        {
            Bind<IUserService>().To<UserService>();

            //Bind<IMapper>().ToMethod(ctx =>
            //{
            //    return new MapperConfiguration(cfg =>
            //    {
            //        cfg.AddProfile<AutomapperProfile>();
            //    }).CreateMapper();
            //});

            Bind<IPhotoService>().To<PhotoService>();
            Bind<IAlbumService>().To<AlbumService>();
            Bind<ILikedEntityService>().To<LikedEntityService>();




        }
    }
}