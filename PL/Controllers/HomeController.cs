using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoService _photoService;

        public HomeController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        public ActionResult Index()
        {
            if(User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            //foreach (var item in User.IsInRole)
            //{
            //    if (item.Value == "admin")
            //        return RedirectToAction("Index", "Admin", new { area = "Admin" });
            //}

            var photos = _photoService.GetAllPhoto();

            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("UserGallery", "Gallery");
            //}

            return View(photos);
        }
    }
}