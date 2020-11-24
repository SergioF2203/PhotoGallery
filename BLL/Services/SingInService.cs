using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Interfaces;

namespace BLL.Services
{
    public class SingInService : ISingInService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SingInService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed && disposing)
            {
                _unitOfWork.Dispose();
            }

            disposed = true;
        }
    }
}
