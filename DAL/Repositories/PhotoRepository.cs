using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly PhotoGalleryContext _context;

        /// <summary>
        /// Ctor Photo Repository
        /// </summary>
        /// <param name="context">PhotoGalleryContext</param>
        public PhotoRepository(PhotoGalleryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add Photo
        /// </summary>
        /// <param name="entity">Photo entity</param>
        public void Add(Photo entity)
        {
            _context.Photos.Add(entity);
        }

        /// <summary>
        /// Add a photo async
        /// </summary>
        /// <param name="entity">Photo entity</param>
        /// <returns></returns>
        public async Task AddAsync(Photo entity)
        {
            await Task.Run(() => _context.Photos.Add(entity));
        }

        /// <summary>
        /// Get all photo
        /// </summary>
        /// <returns></returns>
        public IQueryable<Photo> FindAll()
        {
            return _context.Photos.Include("ApplicationUser");
        }

        /// <summary>
        /// Get a photo by Id async
        /// </summary>
        /// <param name="id">photo's id</param>
        /// <returns>Photo entity</returns>
        public async Task<Photo> FindByIdAsync(string id)
        {
            return await _context.Photos.FindAsync(id);
        }

        /// <summary>
        /// Get photo with publish is true
        /// </summary>
        /// <returns>IEnumerable photo entities</returns>
        public IEnumerable<Photo> GetPublishPhotos()
        {
            return _context.Photos.Where(p => p.IsPublish);
        }

        /// <summary>
        /// Get all photo by rating
        /// </summary>
        /// <returns>IEnumerable photo entities</returns>
        public IEnumerable<Photo> GetPhotoByRating()
        {
            return _context.Photos.OrderByDescending(p => p.Raiting);
        }

        /// <summary>
        /// Remove a photo
        /// </summary>
        /// <param name="entity">photo entity</param>
        public void Remove(Photo entity)
        {
            _context.Photos.Remove(entity);
        }

        /// <summary>
        /// Remove a photo async
        /// </summary>
        /// <param name="entity">photo entity</param>
        /// <returns></returns>
        public async Task RemoveAsync(Photo entity)
        {
            var photo = await FindByIdAsync((entity.Id).ToString());

            _context.Photos.Remove(photo);
        }

        /// <summary>
        /// Update Photo async
        /// </summary>
        /// <param name="entity">Photo entity</param>
        /// <returns></returns>
        public async Task UpdateAsync(Photo entity)
        {
            var photoEntity = await _context.Photos.FindAsync(entity.Id);
            _context.Photos.Attach(photoEntity);
            _context.Entry(photoEntity).State = System.Data.Entity.EntityState.Modified;

        }
    }
}
