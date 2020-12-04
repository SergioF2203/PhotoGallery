using System;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        private readonly PhotoGalleryContext _database;

        /// <summary>
        /// Ctor Client Manager
        /// </summary>
        /// <param name="db">PhtotGalleryContext</param>
        public ClientManager(PhotoGalleryContext db)
        {
            _database = db;
        }

        /// <summary>
        /// Create ClientProfile
        /// </summary>
        /// <param name="item">ClientProfile item</param>
        public void Create(ClientProfile item)
        {
            _database.ClientProfiles.Add(item);
            _database.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing && disposed)
            {
                _database.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Get a client profile by Id async
        /// </summary>
        /// <param name="id">A clint's profile id</param>
        /// <returns>a client profile</returns>
        public async Task<ClientProfile> GetByIdAsync(string id)
        {
            return await _database.ClientProfiles.FindAsync(id);
        }

        /// <summary>
        /// Remove a clien't profile
        /// </summary>
        /// <param name="item">a clien't profile entity</param>
        /// <returns></returns>
        public async Task Remove(ClientProfile item)
        {
            await Task.Run(() => _database.ClientProfiles.Remove(item));

            await _database.SaveChangesAsync();
        }
    }
}
