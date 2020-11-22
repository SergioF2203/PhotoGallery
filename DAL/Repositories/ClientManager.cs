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
    public class ClientManager : IClientManager
    {
        private readonly PhotoGalleryContext _database;

        public ClientManager(PhotoGalleryContext db)
        {
            _database = db;
        }
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

        public async Task<ClientProfile> GetByIdAsync(string id)
        {
            return await _database.ClientProfiles.FindAsync(id);
        }

        public async Task Remove(ClientProfile item)
        {
            await Task.Run(()=>_database.ClientProfiles.Remove(item));

            await _database.SaveChangesAsync();
        }
    }
}
