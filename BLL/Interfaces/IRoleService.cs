﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Infrastructure;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IRoleService : IDisposable
    {
        Task<OperationDetails> Create(string name);
        IEnumerable<RoleDto> GetRoles();
        Task<RoleDto> GetRoleById(string id);
        Task<RoleDto> FindRoleByName(string name);
        Task Remove(string name);
        IEnumerable<UserDto> GetUsersByRole(RoleDto roleDto);
    }
}
