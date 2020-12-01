using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly ILikedEntityService _likedEntityService;

        public HomeController(IPhotoService photoService, ILikedEntityService likedEntityService)
        {
            _photoService = photoService;
            _likedEntityService = likedEntityService;
        }

        public ActionResult Index()
        {
            if(User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }

            //var photos = _photoService.GetAllPhoto();

            var photos = _photoService.GetAllPhotoForLiked(User.Identity.Name);

            return View(photos);
        }

        [HttpPost]
        public async Task<ActionResult> ChangeLike(string photoId, string user)
        {
            await _likedEntityService.AddLikedEntityAsync(photoId, user);

            return RedirectToAction("Index");
        }
    }
}