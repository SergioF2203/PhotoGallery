using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class EditPhotoDto
    {
        public string Id { get; set; }
        public string PhotoPath { get; set; }

        public DateTime DateTimeUploading { get; set; } = default;
        public bool IsPublish { get; set; }
        public int VoiceCount { get; set; }
        public string ThumbnailPath { get; set; }


    }
}
