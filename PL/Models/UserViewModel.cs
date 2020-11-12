using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Models
{
    public class UserViewModel
    {
        public int UserViewModelId { get; set; }
        public string UserViewModelName { get; set; }
        public string UserViewModelEmail { get; set; }
        public string UserViewModelImageRath { get; set; }
    }
}