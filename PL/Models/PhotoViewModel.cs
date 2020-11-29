using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{

    public class GetPhotoViewModel
    {
        [Required]
        public string Id { get; set; }
    }

    public class PhotoEditModel
    {
        public string Id { get; set; }
        public string PhotoPath { get; set; }
    }
}