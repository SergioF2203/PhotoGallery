using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is invalid. Check the data")]
        public string UserEmail { get; set; }

        public string UserImagePath { get; set; }

        [Required]
        public Role Role { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public User()
        {
            Photos = new HashSet<Photo>();
        }
    }
}
