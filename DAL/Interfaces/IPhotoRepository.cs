using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        IEnumerable<Photo> GetPublishPhotos();
        IEnumerable<Photo> GetPhotoByRating();
        IEnumerable<Photo> GetPhotoUserId(string id);
    }
}
