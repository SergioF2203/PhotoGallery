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
    public class CustomRoleRepository : IRoleRepository
    {
        private readonly PhotoGalleryContext _context;

        public CustomRoleRepository(PhotoGalleryContext context)
        {
            _context = context;
        }
        public async Task AddAsync(CustomRole entity)
        {
            if(entity is null)
            {
                throw new ArgumentException("The role entity is invalid", "entity");
            }

            await Task.Run(() => _context.Roles.Add(entity));
        }

        public async Task DeleteAsync(CustomRole entity)
        {
            if(entity is null)
            {
                throw new ArgumentException("The role etity is incorrect", "entity");
            }

            await Task.Run(() =>_context.Roles.Remove(entity));
        }

        public async Task DeleteByIdAsync(int id)
        {
            var removedEntity = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if(removedEntity is null)
            {
                throw new ArgumentException("Can't find the entity by provided id", "id");
            }

            _context.Roles.Remove(removedEntity);
        }

        public IQueryable<CustomRole> FindAll()
        {
            return _context.Roles;
        }

        public async Task FindByIdAsync(int id)
        {
            await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(CustomRole entity)
        {
            var updateEntity = await _context.Roles.FirstOrDefaultAsync(r => r.Id == entity.Id);

            if(updateEntity is null)
            {
                throw new ArgumentException("Can't find a role by Id", "entity");
            }

            updateEntity.UserRole = entity.UserRole;
        }
    }
}
