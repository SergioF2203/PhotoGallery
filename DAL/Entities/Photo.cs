using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public string PhotoTitle { get; set; }
        public DateTime DateTimeUploading { get; set; }
        public Raiting Raiting { get; set; }
    }

    public class Raiting
    {
        public int VoicesCount { get; set; }
        public float CurrentRaiting { get; set; }
    }
}
