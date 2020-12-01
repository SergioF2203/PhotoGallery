using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class LikedEntity
    {
        public string Id { get; set; }
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
