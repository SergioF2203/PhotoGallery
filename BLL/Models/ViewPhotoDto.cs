﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ViewPhotoDto
    {
        public string Id { get; set; }
        public string PhotoPath { get; set; }
        public DateTime DateTimeUploading { get; set; } = default;
        public int VoiceCount { get; set; }
        public string ThumbnailPath { get; set; }
        public string UserName { get; set; }
    }
}
