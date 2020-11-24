﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class PhotoAddViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Path { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
    }
}