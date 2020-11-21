using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDto>().ForMember(u => u.IsLockOut, opt => opt.MapFrom(src => src.LockoutEnabled)).ReverseMap());
            _mapper = new Mapper(config);
        }

        public async Task<ClaimsIdentity> Authenticate(UserDto userDto)
        {
            ClaimsIdentity claims = null;

            var user = await _unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);


            if (user != null)
            {
                if (user.LockoutEnabled && user.LockoutEndDateUtc > DateTime.Now)
                {
                    return claims;
                }

                claims = await _unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            return claims;

        }

        public async Task<OperationDetails> Create(UserDto userDto)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(userDto.Email);

            if (user is null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.UserName };
                var result = await _unitOfWork.UserManager.CreateAsync(user, userDto.Password);

                if (result.Errors.Any())
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), string.Empty);

                await _unitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);

                var clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                _unitOfWork.ClientManager.Create(clientProfile);
                await _unitOfWork.SaveASync();

                return new OperationDetails(true, "Registration Success!", string.Empty);
            }
            else
            {
                return new OperationDetails(false, "The User with such email is already exist", "Email");
            }
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = _unitOfWork.UserManager.Users;

            return _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDto>>(users);
        }

        public async Task SetInitialData(UserDto adminDto, IEnumerable<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await _unitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role is null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await _unitOfWork.RoleManager.CreateAsync(role);
                }
            }

            await Create(adminDto);
        }

        private readonly bool disposed = false;
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
        }

        /// <summary>
        /// Change Lock state User account
        /// </summary>
        /// <param name="email">User's email to lock account</param>
        /// <returns></returns>
        public async Task ChangeLockUserState(string email)
        {
            var user = await _unitOfWork.UserManager.FindByEmailAsync(email);
            var state = user.LockoutEnabled;
            if (state)
            {
                user.LockoutEnabled = false;
                user.LockoutEndDateUtc = null;
            }
            else
            {
                user.LockoutEnabled = true;
                user.LockoutEndDateUtc = DateTime.MaxValue;
            }

            await _unitOfWork.UserManager.UpdateAsync(user);
        }
    }
}
