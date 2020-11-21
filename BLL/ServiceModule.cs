﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
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
            //Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(_connectionString);

        }
    }

}