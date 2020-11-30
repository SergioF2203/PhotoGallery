﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PL.Models;

namespace PL.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IPhotoService _photoService;
        private readonly IUserService _userService;
        private readonly IAlbumService _albumService;
        private Mapper _mapper;
        public GalleryController(IPhotoService photoService, IUserService userService, IAlbumService albumService)
        {
            _photoService = photoService;
            _userService = userService;
            _albumService = albumService;

            //_mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<PhotoAddViewModel, PhotoDto>()
            //.ForMember(dest => dest.PhotoName, opt => opt.MapFrom(src => src.Name))
            //.ReverseMap()));


            //mapping for transfer id and a path of image
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<EditPhotoDto, PhotoEditModel>().ReverseMap()));


        }
        // GET: Gallery
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get all User's phoot
        /// </summary>
        /// <returns>All user's photo in gallery</returns>
        public ActionResult UserGallery()
        {
            var user = _userService.FindUserByName(User.Identity.Name).Result;

            if (user is null)
            {
                return RedirectToAction("LogOff");
            }


            var photoPaths = _photoService.GelAllPhotosPaths(user.Id);


            return View(photoPaths);
        }

        

        public ActionResult UserAlbum()
        {
            var temp = _albumService.GetAlbums().ToList();

            return View(temp);
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

            var userId = _userService.FindUserByName(User.Identity.Name).Result.Id;
            var pic = new PhotoDto() { Id = Guid.NewGuid().ToString(), PhotoName = fileName, DateTimeUploading = DateTime.Now, PhotoPath = path, IsPublish = false, ApplicationUserId = userId };


            _photoService.Add(pic);

            ViewBag.StatusMessage = "File successfully uploaded!";

            return RedirectToAction("GetAllPath", "Gallery", new { userId = pic.ApplicationUserId });
        }

        [HttpPost]
        public ActionResult AddAlbum (string albumTitle)
        {
            var album = new AlbumDto() {Id = Guid.NewGuid().ToString(), Title = albumTitle };

            _albumService.Add(album);

            return RedirectToAction("UserAlbum");
        }

        public async Task<ActionResult> RemovePhoto(string photoId)
        {
            var photo = await _photoService.GetPhotoByIdAsync(photoId);

            if (System.IO.File.Exists(photo.PhotoPath))
            {
                System.IO.File.Delete(photo.PhotoPath);
            }

            _photoService.Remove(photo);

            return RedirectToAction("UserGallery");
        }

        public async Task<ActionResult> RemoveAlbum(string albumId)
        {
            var album = await _albumService.GetAlbumByIdAsync(albumId);

            _albumService.Remove(album);

            return RedirectToAction("UserAlbum");
        }

        public ActionResult GetAllPath(string userId)
        {
            var temp = _photoService.GelAllPhotosPaths(userId);

            //var mappedEntites = _mapper.Map<IEnumerable<EditPhotoDto>, IEnumerable<PhotoEditModel>>(temp);

            return View("UserGallery", temp);
        }

        public ActionResult GetPhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetPhoto(GetPhotoViewModel model)
        {
            var temp = await _photoService.GetPhotoByIdAsync(model.Id);

            return View();
        }

        /// <summary>
        /// Change photo visibility
        /// </summary>
        /// <param name="photoId">Photo's Id</param>
        public async Task<ActionResult> ChangeVisibility(string photoId)
        {
            var photo = await _photoService.GetPhotoByIdAsync(photoId);

            await _photoService.ChangeVisibilityAsync(photoId);

            return RedirectToAction("UserGallery");
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }
}