using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
        private readonly Mapper _mapper;
        public GalleryController(IPhotoService photoService, IUserService userService, IAlbumService albumService)
        {
            _photoService = photoService;
            _userService = userService;
            _albumService = albumService;

            //mapping for transfer id and a path of image
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<EditPhotoDto, PhotoEditModel>().ReverseMap()));
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Gallery
        public ActionResult Index()
        {
            return RedirectToAction("UserGallery");
        }

        /// <summary>
        /// Get all User's photo
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

        /// <summary>
        /// Add photo ti user's gallery
        /// </summary>
        /// <param name="file">a file to  upload</param>
        /// <returns></returns>
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
                //max image size
                int maxImageSize = int.Parse(ConfigurationManager.AppSettings["MaxByteImageSize"]);

                if (fileSize > maxImageSize * 1024)
                {
                    TempData["Message"] = "file is too big";
                    return RedirectToAction("UserGallery", "Gallery");
                }

                var fileExtention = Path.GetExtension(file.FileName).ToLower();

                //valid exeptions
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
                    TempData["Message"] = "File isn't a picture";

                    return RedirectToAction("UserGallery");
                }

                fileName = Path.GetFileName(file.FileName);
                //upload path from app.settings
                var uploadPath = ConfigurationManager.AppSettings["UploadImagePath"];

                var dirPath = Path.Combine(Server.MapPath(uploadPath), User.Identity.GetUserId());


                path = Path.Combine(dirPath, fileName);

                //check exist directory
                if (Directory.Exists(dirPath))
                {
                    if (System.IO.File.Exists(path))
                    {
                        TempData["Message"] = "The file with same name ia already exist";

                        return RedirectToAction("UserGallery");
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

                    using (var wrapMode = new ImageAttributes())
                    {
                        wrapMode.SetWrapMode(System.Drawing.Drawing2D.WrapMode.TileFlipXY);
                        g.DrawImage(src, cropRectangle, 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, wrapMode);
                    }
                }

                //thumbnail file name
                thumbFileName = Path.GetFileNameWithoutExtension(file.FileName) + "_thumb";
                thumbFileName += Path.GetExtension(file.FileName);

                //thumbnail path
                var thumbPath = Path.Combine(Server.MapPath(uploadPath + User.Identity.GetUserId()), "thumbnail");
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

        /// <summary>
        /// Add an album
        /// </summary>
        /// <param name="albumTitle">album's title</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAlbum(string albumTitle)
        {
            var album = new AlbumDto() { Id = Guid.NewGuid().ToString(), Title = albumTitle };

            _albumService.Add(album);

            return RedirectToAction("UserAlbum");
        }

        /// <summary>
        /// Remove a photo from gallery and server
        /// </summary>
        /// <param name="photoId">photo's id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Remove an album
        /// </summary>
        /// <param name="albumId">album's id</param>
        /// <returns></returns>
        public async Task<ActionResult> RemoveAlbum(string albumId)
        {
            var album = await _albumService.GetAlbumByIdAsync(albumId);

            _albumService.Remove(album);

            return RedirectToAction("UserAlbum");
        }

        /// <summary>
        /// Get all photos' path for single user
        /// </summary>
        /// <param name="userId">user's id</param>
        /// <returns></returns>
        public ActionResult GetAllPath(string userId)
        {
            var temp = _photoService.GelAllPhotosPaths(userId);

            return View("UserGallery", temp);
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

        /// <summary>
        /// Get photo in user's album
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Album()
        {
            var user = await _userService.FindUserByName(User.Identity.Name);

            var photoPaths = _photoService.GelAllPhotosPaths(user.Id);

            return View(photoPaths);

        }

        public ActionResult LikedEntity()
        {
            var user = _userService.FindUserByName(User.Identity.Name).Result;

            var photos = _photoService.GetPhotoByUserId(user.Id.ToString());

            return View(photos);
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}