﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Areas.User.Controllers
{
    public class UserController : Controller
    {
        // GET: User/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}