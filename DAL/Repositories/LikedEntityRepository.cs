using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    class LikedEntityRepository : ILikedEntityRepository
    {
        private readonly PhotoGalleryContext _context;

        /// <summary>
        /// Ctor LinkedEntity
        /// </summary>
        /// <param name="context">PhotoGalleryContext</param>
        public LikedEntityRepository(PhotoGalleryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add liked entity
        /// </summary>
        /// <param name="entity">liked entity</param>
        public void Add(LikedEntity entity)
        {
            _context.LikedEntities.Add(entity);
        }

        /// <summary>
        /// Add liked entity
        /// </summary>
        /// <param name="entity">liked entity</param>
        /// <returns></returns>
        public Task AddAsync(LikedEntity entity)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get all liked entity
        /// </summary>
        /// <returns></returns>
        public IQueryable<LikedEntity> FindAll()
        {
            return _context.LikedEntities.Include("Users");
        }

        /// <summary>
        /// Get a liked entity by Id
        /// </summary>
        /// <param name="id">liked entity id</param>
        /// <returns></returns>
        public async Task<LikedEntity> FindByIdAsync(string id)
        {
            return await Task.Run(() => _context.LikedEntities.Include("Users").FirstOrDefault(e => e.Id == id));
        }

        /// <summary>
        /// Remove liked entity
        /// </summary>
        /// <param name="entity">liked entity</param>
        public void Remove(LikedEntity entity)
        {
            _context.LikedEntities.Remove(entity);
        }

        /// <summary>
        /// Remove a liked entity async
        /// </summary>
        /// <param name="entity">liked entity</param>
        /// <returns></returns>
        public Task RemoveAsync(LikedEntity entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update liked entity
        /// </summary>
        /// <param name="entity">liked entity for update</param>
        /// <returns></returns>
        public Task UpdateAsync(LikedEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
