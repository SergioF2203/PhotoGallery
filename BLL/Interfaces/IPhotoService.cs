﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IPhotoService : IDisposable
    {
        Task AddAsync(PhotoDto model);
    }
}