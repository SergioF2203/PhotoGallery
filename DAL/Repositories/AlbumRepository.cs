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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly PhotoGalleryContext _context;

        public AlbumRepository(PhotoGalleryContext context)
        {
            _context = context;
        }

        public void Add(Album entity)
        {
            _context.Albums.Add(entity);
        }

        public Task AddAsync(Album entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Album> FindAll()
        {
            return _context.Albums;
        }

        public async Task<Album> FindByIdAsync(string id)
        {
            return await _context.Albums.FindAsync(id);
        }

        public void Remove(Album entity)
        {
            _context.Albums.Remove(entity);
        }

        public async Task RemoveAsync(Album entity)
        {
            var album =await  _context.Albums.FindAsync(entity.Id.ToString());
            _context.Albums.Remove(album);
        }

        public Task UpdateAsync(Album entity)
        {
            throw new NotImplementedException();
        }
    }
}
