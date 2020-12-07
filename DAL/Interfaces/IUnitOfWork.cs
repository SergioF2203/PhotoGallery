using System;
using System.Threading.Tasks;
using DAL.Identity;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }

        IPhotoRepository PhotoRepository { get; }
        IAlbumRepository AlbumRepository { get; }
        ILikedEntityRepository LikedEntityRepository { get; }
        Task SaveASync();
    }
}
