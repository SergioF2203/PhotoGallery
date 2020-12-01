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
    class LikedEntityRepository : ILikedEntityRepository
    {
        private readonly PhotoGalleryContext _context;

        public LikedEntityRepository(PhotoGalleryContext context)
        {
            _context = context;
        }
        public void Add(LikedEntity entity)
        {
            _context.LikedEntities.Add(entity);
        }

        public Task AddAsync(LikedEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<LikedEntity> FindAll()
        {
            return _context.LikedEntities.Include("Users");
        }

        public async Task<LikedEntity> FindByIdAsync(string id)
        {
            return await Task.Run(()=> _context.LikedEntities.Include("Users").FirstOrDefault(e=>e.Id==id));
        }

        public void Remove(LikedEntity entity)
        {
            _context.LikedEntities.Remove(entity);
        }

        public Task RemoveAsync(LikedEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(LikedEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
