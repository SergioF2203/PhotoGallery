using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly PhotoGalleryContext _context;
        public UserRepository(PhotoGalleryContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CustomUser entity)
        {
            await Task.Run(() => _context.Users.Add(entity));
        }

        public async Task DeleteAsync(CustomUser entity)
        {
            var removedItem = await _context.Users.FirstOrDefaultAsync(e => e.Id == entity.Id);

            if (removedItem is null)
            {
                throw new ArgumentException("The argument is not correct", "entity");
            }

            await UpdateAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var removeEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (removeEntity is null)
            {
                throw new ArgumentException("Can't find a user", "id");
            }

            removeEntity.IsRemoved = true;
            await UpdateAsync(removeEntity);
        }

        public IQueryable<CustomUser> FindAll()
        {
            return _context.Users;
        }

        public async Task FindByIdAsync(int id)
        {
            await Task.Run(() => _context.Users.Find(id));
        }

        public async Task UpdateAsync(CustomUser entity)
        {
            var currentEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == entity.Id);

            if (currentEntity is null)
            {
                throw new ArgumentException("The entity of User is incorrect", "entity");
            }

            currentEntity.Role = entity.Role;
            currentEntity.UserEmail = entity.UserEmail;
            currentEntity.IsRemoved = entity.IsRemoved;
            currentEntity.UserImagePath = entity.UserImagePath;
            currentEntity.UserName = entity.UserName;
        }
    }
}
