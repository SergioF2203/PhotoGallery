using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAlbumService : IDisposable
    {
        void Add(AlbumDto model);
        void Remove(AlbumDto model);
        IEnumerable<AlbumDto> GetAlbums();
        Task<AlbumDto> GetAlbumByIdAsync(string albumId);
    }
}
