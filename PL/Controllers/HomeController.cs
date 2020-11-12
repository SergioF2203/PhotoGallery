using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        //private PhotoGalleryContext db = new PhotoGalleryContext();
        public ActionResult Index()
        {
            //var user1Photos = new List<Photo>() 
            //{ 
            //    new Photo 
            //    { 
            //        Id = 0,
            //        PhotoTitle = "photoTitle1",
            //        PhotoPath = "https://images.pexels.com/photos/4710814/pexels-photo-4710814.jpeg?cs=srgb&dl=pexels-eugene-golovesov-4710814.jpg&fm=jpg" },
            //    new Photo
            //    {
            //        Id = 1,
            //        PhotoTitle = "photoTitl2",
            //        PhotoPath = "https://images.pexels.com/photos/5277693/pexels-photo-5277693.jpeg?cs=srgb&dl=pexels-daniel-torobekov-5277693.jpg&fm=jpg"
            //    }
            //};

            //var u1 = new User { 
            //    Id = 0, 
            //    Role = new Role { 
            //        Id = 0, 
            //        UserRole = "Guest" }, 
            //    UserEmail = "u1@gemail.com", 
            //    UserName = "user1", 
            //    Photos = user1Photos, 
            //    UserImagePath = null 
            //};

            //var model = new List<User>();
            //model.Add(u1);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}