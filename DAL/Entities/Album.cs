using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Album
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string AlbumName { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
