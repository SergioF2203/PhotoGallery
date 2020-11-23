using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Photo : BaseEntity
    {
        [Required]
        [DataType(DataType.ImageUrl, ErrorMessage ="The Image's path i not correct, check the data please")]
        public string PhotoPath { get; set; }

        public string PhotoTitle { get; set; }
        public DateTime DateTimeUploading { get; set; }
        public Raiting Raiting { get; set; }
        public ExifData ExifData { get; set; }
        public bool IsPublish { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Album> Albums { get; set; } = new List<Album>();

    }

    public class Raiting
    {
        public int VoicesCount { get; set; }
        public float CurrentRaiting { get; set; }
    }


    public class ExifData
    {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
