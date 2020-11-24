using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.Models;

namespace PL.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IPhotoService _photoService;
        public GalleryController(IPhotoService photoService)
        {
            _photoService = photoService;
        }
        // GET: Gallery
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPhoto(PhotoDto photoDto)
        {
            _photoService.AddAsync(photoDto);

            return View(photoDto);
        }
    }
}