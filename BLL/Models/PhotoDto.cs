using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PhotoDto
    {
        public string Id { get; set; }
        public string PhotoPath { get; set; }
        public string PhotoTitle { get; set; }
        public DateTime DateTimeUploading { get; set; }
    }
}
