using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class LikedEntityService : ILikedEntityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikedEntityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task TogleLikedStateAsync(string entityId, string userName)
        {
            var photo = await _unitOfWork.PhotoRepository.FindByIdAsync(entityId);

            //check exist entity in DB
            if (_unitOfWork.LikedEntityRepository.FindAll().Any(e => e.Id == entityId))
            {
                //check exist user in collection
                var entity = await _unitOfWork.LikedEntityRepository.FindByIdAsync(entityId);

                if (entity.Users.Any(u => u.UserName == userName))
                {
                    var user = await _unitOfWork.UserManager.FindByNameAsync(userName);

                    //remove user from collection
                    entity.Users.Remove(user);

                    //decrease like count in photo data
                    photo.Raiting.VoicesCount--;
                }
                else
                {
                    entity.Users.Add(await _unitOfWork.UserManager.FindByNameAsync(userName));
                    //increase like count
                    photo.Raiting.VoicesCount++;
                }

                //check cpacity collection
                //if capacity = 0, remove entity from table
                if (entity.Users.Count == 0)
                {
                    _unitOfWork.LikedEntityRepository.Remove(entity);
                }

                await _unitOfWork.SaveASync();
            }
            else
            {
                var tempEntity = new LikedEntity() { Id = entityId };
                tempEntity.Users.Add(await _unitOfWork.UserManager.FindByNameAsync(userName));

                _unitOfWork.LikedEntityRepository.Add(tempEntity);
                //increase like count
                photo.Raiting.VoicesCount++;

                await _unitOfWork.SaveASync();
            }
        }


        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                _unitOfWork.Dispose();

            disposed = true;

        }
    }
}
