﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Infrastructure;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDto userDto);
        Task<ClaimsIdentity> Authenticate(UserDto userDto);
        IEnumerable<UserDto> GetUsers();
        Task<UserDto> FindUserByName(string name);
        Task SetInitialData(UserDto adminDto, IEnumerable<string> roles);
        Task ChangeLockUserState(string email);
        Task RemoveUserAsync(string email);
        Task<OperationDetails> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        UserDto FindById(string id);
        Task<UserDto> FindByIdAsync(string id);
    }
}
