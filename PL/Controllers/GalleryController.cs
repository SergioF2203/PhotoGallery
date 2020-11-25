using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using PL.Models;

namespace PL.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IPhotoService _photoService;
        private Mapper _mapper;
        public GalleryController(IPhotoService photoService)
        {
            _photoService = photoService;

            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PhotoAddViewModel, PhotoDto>()
            .ForMember(dest => dest.PhotoName, opt => opt.MapFrom(src => src.Name))
            .ReverseMap()));


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
        public ActionResult AddPhoto(HttpPostedFileBase file)
        {
            var fileName = string.Empty;
            var path = string.Empty;

            if (file != null)
            {
                var fileSize = file.ContentLength;
                if (fileSize > 1000 * 1024)
                {
                    ModelState.AddModelError("size", "File is too big");
                    return View();
                }

                var fileExtention = Path.GetExtension(file.FileName).ToLower();
                var listOfExtensions = new List<string>() { ".jpg", ".jpeg", ".bmp", ".png" };
                var isAccept = false;

                foreach (var item in listOfExtensions)
                {
                    if (fileExtention == item)
                    {
                        isAccept = true;
                        break;
                    }
                }

                if (!isAccept)
                {
                    ModelState.AddModelError("extension", "File isn't a picture");
                    return View();
                }

                fileName = Path.GetFileName(file.FileName);
                var dirPath = Path.Combine(Server.MapPath("~/Upload/"), User.Identity.GetUserId());
                path = Path.Combine(dirPath, fileName);

                if (Directory.Exists(dirPath))
                {
                    if (System.IO.File.Exists(path))
                    {
                        ModelState.AddModelError("file", "The file with same name ia already exist");
                        return View();
                    }
                    else
                    {
                        file.SaveAs(path);
                    }
                }
                else
                {
                    Directory.CreateDirectory(dirPath);
                    file.SaveAs(path);
                }
            }

            var pic = new PhotoDto() { Id = Guid.NewGuid().ToString(), PhotoName = fileName, DateTimeUploading = DateTime.Now, PhotoPath = path, IsPublish = false, CreateDate = DateTime.Now };
            _photoService.AddAsync(pic);

            ViewBag.StatusMessage = "File successfully uploaded!";
            return View();
        }
    }
}