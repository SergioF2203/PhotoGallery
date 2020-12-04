using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Not Found Result
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// Forbidden Result
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;
            return View();
        }
    }
}