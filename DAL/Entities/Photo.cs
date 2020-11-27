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
        public string PhotoPath { get; set; }

        public string PhotoTitle { get; set; }
        public DateTime DateTimeUploading { get; set; } = default;
        public Raiting Raiting { get; set; } = new Raiting();
        public bool IsPublish { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Album> Albums { get; set; } = new List<Album>();

    }

    public class Raiting
    {
        public int VoicesCount { get; set; } = 0;
        public float CurrentRaiting { get; set; } = 0;
    }
}
