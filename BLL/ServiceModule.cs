using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Ninject.Modules;

namespace BLL
{
    public class ServiceModule : NinjectModule
    {
        private readonly string _connectionString;

        public ServiceModule(string connectionString)
        {
            _connectionString = connectionString;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(_connectionString);

            //Bind<IPhotoService>().To<PhotoService>();


            //Bind<IMapper>().ToMethod(ctx =>
            //{
            //    return new MapperConfiguration(cfg =>
            //    {
            //        cfg.AddProfile<AutoMap>();
            //    }).CreateMapper();
            //});

        }


    }

}
