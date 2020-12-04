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
        }
    }

}
