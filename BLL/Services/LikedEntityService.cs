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

        public async Task AddLikedEntityAsync(string entityId, string userName)
        {
            var tempEntity = new LikedEntity() { Id = entityId };
            tempEntity.Users.Add(await _unitOfWork.UserManager.FindByNameAsync(userName));

            _unitOfWork.LikedEntityRepository.Add(tempEntity);
            await _unitOfWork.SaveASync();
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
