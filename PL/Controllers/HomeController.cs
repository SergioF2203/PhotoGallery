using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Models;

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

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole("admin"))
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }

            if (User.Identity.IsAuthenticated)
            {
                return View(await _photoService.GetAllPhotoForLiked(User.Identity.Name));
            }
            else
            {
                return View(_photoService.GetAllPhoto());
            }
        }

        [HttpPost]
        public async Task<ActionResult> ChangeLike(string photoId, string user)
        {
            await _likedEntityService.TogleLikedStateAsync(photoId, user);

            return RedirectToAction("Index");
        }
    }
}