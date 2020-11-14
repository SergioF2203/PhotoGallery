using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Identity;
using DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private readonly PhotoGalleryContext db;

        private readonly ApplicationUserManager userManager;
        private readonly ApplicationRoleManager roleManager;
        private readonly IClientManager clientManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new PhotoGalleryContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }
        public ApplicationUserManager UserManager => userManager;

        public IClientManager ClientManager => clientManager;

        public ApplicationRoleManager RoleManager => roleManager;

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                userManager.Dispose();
                roleManager.Dispose();
                clientManager.Dispose();
            }

            this.disposed = true;
        }

        public async Task SaveASync()
        {
            await db.SaveChangesAsync();
        }
    }
}
