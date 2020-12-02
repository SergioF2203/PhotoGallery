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
        public string PhotoName { get; set; }
        public string PhotoTitle { get; set; }
        public DateTime DateTimeUploading { get; set; }
        public string PhotoPath { get; set; }
        public bool IsPublish { get; set; }
        public int VoiceCount { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
