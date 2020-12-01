using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class LikedEntity : BaseEntity
    {
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
