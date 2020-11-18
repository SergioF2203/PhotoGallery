using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationDetails> Create(string name)
        {
            var role = await _unitOfWork.RoleManager.FindByNameAsync(name);

            if(role is null)
            {
                role = new ApplicationRole { Name = name };
                var result = await _unitOfWork.RoleManager.CreateAsync(role);

                if (result.Errors.Any())
                {
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), string.Empty);
                }


            }

            await _unitOfWork.SaveASync();

            return new OperationDetails(true, "The role added", string.Empty);

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
