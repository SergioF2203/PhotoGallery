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
    public class PhotoRepository : IPhotoRepository
    {
        private readonly PhotoGalleryContext _context;

        public PhotoRepository(PhotoGalleryContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(Photo entity)
        {
            await Task.Run(()=>_context.Photos.Add(entity));
        }

        public IQueryable<Photo> FindAll()
        {
            return _context.Photos;
        }

        public async Task<Photo> FindByIdAsync(string id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public async Task RemoveAsync(Photo entity)
        {
            var photo = await FindByIdAsync((entity.Id).ToString());

            await Task.Run(()=>_context.Photos.Remove(photo));
        }

        public Task UpdateAsync(Photo entity)
        {
            throw new NotImplementedException();
        }
    }
}
