using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IPhotoService : IDisposable
    {
        Task AddAsync(PhotoDto model);
        void Add(PhotoDto model);
        void Remove(PhotoDto model);

        Task<PhotoDto> GetPhotoByIdAsync(string id);
        IEnumerable<string> GetPathsPublishPhoto();
        IEnumerable<EditPhotoDto> GelAllPhotosPaths(string id);




    }
}
