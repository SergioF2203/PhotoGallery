using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Role : BaseEntity
    {
        //[Required]
        //public int RoleId { get; set; }
        
        [Required]
        public string UserRole { get; set; }
    }
}
