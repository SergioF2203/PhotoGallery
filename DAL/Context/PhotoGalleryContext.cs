using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Context
{
    public class PhotoGalleryContext : IdentityDbContext<ApplicationUser>
    {
        public PhotoGalleryContext() : base("DefaultConnection") { }

        public PhotoGalleryContext(string connection) : base(connection) { }


        public static PhotoGalleryContext Create()
        {
            return new PhotoGalleryContext();
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Photo> Photos { get; set; }
        public DbSet<Album> Albums { get; set; }
    }
}
