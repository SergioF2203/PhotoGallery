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
        void AddAsync(PhotoDto model);


    }
}
