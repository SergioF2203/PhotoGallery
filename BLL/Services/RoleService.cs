using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMap;

        /// <summary>
        /// Ctor Role Service
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork interface</param>
        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationRole, RoleDto>().ReverseMap());
            _autoMap = new Mapper(config);

        }

        /// <summary>
        /// Async create OperationDetails
        /// </summary>
        /// <param name="name">role's name</param>
        /// <returns>Operation Details</returns>
        public async Task<OperationDetails> Create(string name)
        {
            var role = await _unitOfWork.RoleManager.FindByNameAsync(name);

            if (role is null)
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

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>IEnumerable Role's Dto</returns>
        public IEnumerable<RoleDto> GetRoles()
        {
            var roles = _unitOfWork.RoleManager.Roles;

            return _autoMap.Map<IEnumerable<ApplicationRole>, IEnumerable<RoleDto>>(roles);
        }

        /// <summary>
        /// Get single role by id
        /// </summary>
        /// <param name="id">role's id</param>
        /// <returns>Role's Dto</returns>
        public async Task<RoleDto> GetRoleById(string id)
        {
            var role = await _unitOfWork.RoleManager.FindByIdAsync(id);

            return _autoMap.Map<ApplicationRole, RoleDto>(role);
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
            {
                _unitOfWork.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Get all users for specific role
        /// </summary>
        /// <param name="roleDto">Role model</param>
        /// <returns>IEnumerable User's Dto</returns>
        public IEnumerable<UserDto> GetUsersByRole(RoleDto roleDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Role
        /// </summary>
        /// <param name="name">Role's name</param>
        /// <returns>Role Data Transfer Object</returns>
        public async Task<RoleDto> FindRoleByName(string name)
        {
            var role = await _unitOfWork.RoleManager.FindByNameAsync(name);

            return _autoMap.Map<ApplicationRole, RoleDto>(role);
        }

        /// <summary>
        /// Remove a role
        /// </summary>
        /// <param name="name">Role's name</param>
        /// <returns></returns>
        public async Task Remove(string name)
        {
            var role = await _unitOfWork.RoleManager.FindByNameAsync(name);
            await _unitOfWork.RoleManager.DeleteAsync(role);
            await _unitOfWork.SaveASync();
        }
    }
}
