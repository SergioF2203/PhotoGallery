using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}