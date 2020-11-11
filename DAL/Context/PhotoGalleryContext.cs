﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Context
{
    public class PhotoGalleryContext : DbContext
    {
        public PhotoGalleryContext() : base() { }
        public PhotoGalleryContext(string connectionString) : base(connectionString) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}