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
            return _context.LikedEntities;
        }

        public Task<LikedEntity> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(LikedEntity entity)
        {
            throw new NotImplementedException();
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
