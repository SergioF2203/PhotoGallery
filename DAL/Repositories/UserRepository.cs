using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PhotoGalleryContext context;
        public UserRepository(PhotoGalleryContext _context)
        {
            context = _context;
        }

        public async Task AddAsync(User entity)
        {
            await Task.Run(() => context.Users.Add(entity));
        }
    }
}
