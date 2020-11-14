using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL
{
    public class UnitOfWork /*: IUnitOfWork*/
    {
        private readonly PhotoGalleryContext context;

        //private IUserRepository userRepository;
        //private IRoleRepository roleRepository;

        public UnitOfWork(string connectionString)
        {
            context = new PhotoGalleryContext(connectionString);
        }
        //public IUserRepository UserRepository
        //{
        //    get
        //    {
        //        if(userRepository is null)
        //        {
        //            userRepository = new UserRepository(context);
        //        }
        //        return userRepository;
        //    }
        //}

        //public IRoleRepository RoleRepository
        //{
        //    get
        //    {
        //        if(roleRepository is null)
        //        {
        //            roleRepository = new CustomRoleRepository(context);
        //        }
        //        return roleRepository;
        //    }
        //}

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
