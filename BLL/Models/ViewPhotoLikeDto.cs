using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ViewPhotoLikeDto
    {
        public string Id { get; set; }
        public string PhotoPath { get; set; }

        public DateTime DateTimeUploading { get; set; } = default;
        public bool IsLiked { get; set; }
    }
}
