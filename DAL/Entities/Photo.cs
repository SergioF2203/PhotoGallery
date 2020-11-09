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
        public Album Album { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class Raiting
    {
        public int VoicesCount { get; set; }
        public float CurrentRaiting { get; set; }
    }

    public class Album
    {
        public string AlbumName { get; set; }
    }

    public class MetaData
    {

    }
}
