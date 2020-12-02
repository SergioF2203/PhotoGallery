using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
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
            var thumbFileName = string.Empty;
            var thumbFilePath = string.Empty;

            //check exist file from from
            if (file != null)
            {
                var fileSize = file.ContentLength;

                //check file size
                if (fileSize > 1000 * 1024)
                {
                    ModelState.AddModelError("size", "File is too big");
                    return View();
                }

                var fileExtention = Path.GetExtension(file.FileName).ToLower();
                //var listOfExtensions = new List<string>() { ".jpg", ".jpeg", ".bmp", ".png" };
                string[] listOfExtensions = ConfigurationManager.AppSettings["ValidImageExtensions"].Split(',');
                var isAccept = false;

                //check valid file extension
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

                //check exist directory
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

                //creates thumbnail
                Rectangle cropRectangle = new Rectangle(0, 0, 420, 236);
                Bitmap src = Image.FromStream(file.InputStream, true, true) as Bitmap;
                Bitmap target = new Bitmap(cropRectangle.Width, cropRectangle.Height);

                target.SetResolution(src.HorizontalResolution, src.VerticalResolution);

                using (Graphics g = Graphics.FromImage(target))
                {
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                    using(var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                        g.DrawImage(src, cropRectangle, 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                //thumbnail file name
                thumbFileName = Path.GetFileNameWithoutExtension(file.FileName) + "_thumb";
                thumbFileName += Path.GetExtension(file.FileName);

                //thumbnail path
                var thumbPath = Path.Combine(Server.MapPath("~/Upload/" + User.Identity.GetUserId()), "thumbnail");
                thumbFilePath = Path.Combine(thumbPath, thumbFileName);


                //check exist directory
                if (Directory.Exists(thumbPath))
                {
                    if (!System.IO.File.Exists(thumbFilePath))
                    {
                        target.Save(thumbFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                }
                else
                {
                    Directory.CreateDirectory(thumbPath);
                    target.Save(thumbFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }

            }

            var userId = _userService.FindUserByName(User.Identity.Name).Result.Id;

            var pic = new PhotoDto()
            {
                Id = Guid.NewGuid().ToString(),
                PhotoName = fileName,
                DateTimeUploading = DateTime.Now,
                PhotoPath = path,
                ThumbnailPath = thumbFilePath,
                IsPublish = false,
                ApplicationUserId = userId
            };


            _photoService.Add(pic);

            ViewBag.StatusMessage = "File successfully uploaded!";

            return RedirectToAction("GetAllPath", "Gallery", new { userId = pic.ApplicationUserId });
        }

        [HttpPost]
        public ActionResult AddAlbum(string albumTitle)
        {
            var album = new AlbumDto() { Id = Guid.NewGuid().ToString(), Title = albumTitle };

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
            if (System.IO.File.Exists(photo.ThumbnailPath))
            {
                System.IO.File.Delete(photo.ThumbnailPath);
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

        public async Task<ActionResult> Album()
        {
            var user = await _userService.FindUserByName(User.Identity.Name);

            var photoPaths = _photoService.GelAllPhotosPaths(user.Id);

            return View(photoPaths);

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