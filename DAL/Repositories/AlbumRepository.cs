using System;
using System.Linq;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly PhotoGalleryContext _context;

        /// <summary>
        /// Ctor Album Repository
        /// </summary>
        /// <param name="context">PhotoGalleryContext</param>
        public AlbumRepository(PhotoGalleryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add Album
        /// </summary>
        /// <param name="entity">Album entity</param>
        public void Add(Album entity)
        {
            _context.Albums.Add(entity);
        }

        /// <summary>
        /// Add album async
        /// </summary>
        /// <param name="entity">Album entity</param>
        /// <returns></returns>
        public Task AddAsync(Album entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all albums
        /// </summary>
        /// <returns></returns>
        public IQueryable<Album> FindAll()
        {
            return _context.Albums;
        }

        /// <summary>
        /// Get an album by Id async
        /// </summary>
        /// <param name="id">album's Id</param>
        /// <returns>Album</returns>
        public async Task<Album> FindByIdAsync(string id)
        {
            return await _context.Albums.FindAsync(id);
        }

        /// <summary>
        /// Remove an album
        /// </summary>
        /// <param name="entity">Album entity</param>
        public void Remove(Album entity)
        {
            _context.Albums.Remove(entity);
        }

        /// <summary>
        /// Remove an album async
        /// </summary>
        /// <param name="entity">Album entity</param>
        /// <returns></returns>
        public async Task RemoveAsync(Album entity)
        {
            var album = await _context.Albums.FindAsync(entity.Id.ToString());
            _context.Albums.Remove(album);
        }

        /// <summary>
        /// Update an album entity
        /// </summary>
        /// <param name="entity">An album entity for udpate</param>
        /// <returns></returns>
        public Task UpdateAsync(Album entity)
        {
            throw new NotImplementedException();
        }
    }
}
