using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

    }
}