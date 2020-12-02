using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly Mapper _mapper;

        public PhotoService(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Photo, PhotoDto>().ForMember(dst => dst.VoiceCount, opt => opt.MapFrom(scr => scr.Raiting.VoicesCount)).ReverseMap();
                cfg.CreateMap<PhotoDto, EditPhotoDto>().ReverseMap();
                cfg.CreateMap<Photo, ViewPhotoDto>().ForMember(dst => dst.VoiceCount, opt => opt.MapFrom(src => src.Raiting.VoicesCount));
                cfg.CreateMap<Photo, ViewPhotoLikeDto>().ForMember(dst => dst.VoiceCount, opt => opt.MapFrom(src => src.Raiting.VoicesCount));
            }));
        }
        public async Task AddAsync(PhotoDto model)
        {
            await _unitOfwork.PhotoRepository.AddAsync(_mapper.Map<PhotoDto, Photo>(model));
            await _unitOfwork.SaveASync();
        }

        public void Add(PhotoDto model)
        {
            _unitOfwork.PhotoRepository.Add(_mapper.Map<PhotoDto, Photo>(model));
            _unitOfwork.SaveASync();
        }

        public async Task<PhotoDto> GetPhotoByIdAsync(string id)
        {
            var photo = await _unitOfwork.PhotoRepository.FindByIdAsync(id);

            return _mapper.Map<Photo, PhotoDto>(photo);
        }

        public async Task<IEnumerable<ViewPhotoLikeDto>> GetAllPhotoForLiked(string userName)
        {
            var photos = _unitOfwork.PhotoRepository.FindAll().Where(p => p.IsPublish).OrderByDescending(p => p.DateTimeUploading).ToList();

            var photosDto = _mapper.Map<IEnumerable<Photo>, IEnumerable<ViewPhotoLikeDto>>(photos);

            _unitOfwork.LikedEntityRepository.FindAll().ToList();

            //var usersLikedPhoto = _unitOfwork.UserManager.FindByNameAsync(userName).Result.LikedPhoto.ToList();

            var user = await _unitOfwork.UserManager.FindByNameAsync(userName);
            var usersLikedPhoto = user.LikedPhoto.ToList();

            if (usersLikedPhoto.Count != 0)
            {
                foreach (var item in photosDto)
                {
                    if (usersLikedPhoto.Any(p => p.Id == item.Id))
                    {
                        item.IsLiked = true;
                    }
                }
            }

            var customPhotoList = new List<ViewPhotoLikeDto>();


            foreach (var item in photosDto)
            {
                var index = item.ThumbnailPath.IndexOf("Upload");
                var cutedPath = item.ThumbnailPath.Substring(index);

                customPhotoList.Add(new ViewPhotoLikeDto { Id = item.Id, DateTimeUploading = item.DateTimeUploading, ThumbnailPath = cutedPath, IsLiked = item.IsLiked, VoiceCount = item.VoiceCount });
            }

            return customPhotoList;
        }

        public IEnumerable<ViewPhotoDto> GetAllPhoto()
        {
            var photos = _unitOfwork.PhotoRepository.FindAll().Where(p => p.IsPublish).OrderByDescending(p => p.DateTimeUploading).ToList();

            var photosDto = _mapper.Map<IEnumerable<Photo>, IEnumerable<ViewPhotoDto>>(photos);

            var customPhotoList = new List<ViewPhotoDto>();

            foreach (var item in photosDto)
            {
                var index = item.ThumbnailPath.IndexOf("Upload");
                var cutedPath = item.ThumbnailPath.Substring(index);

                customPhotoList.Add(new ViewPhotoDto { Id = item.Id, DateTimeUploading = item.DateTimeUploading, ThumbnailPath = cutedPath, VoiceCount = item.VoiceCount });
            }

            return customPhotoList;
        }


        public IEnumerable<EditPhotoDto> GelAllPhotosPaths(string id)
        {
            var photos = _unitOfwork.PhotoRepository.FindAll().Where(p => p.ApplicationUserId == id);

            var photoFullPath = _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDto>>(photos);

            var editPhotosList = new List<EditPhotoDto>();

            foreach (var item in photoFullPath)
            {
                var index = item.ThumbnailPath.IndexOf("Upload");
                var cutedPath = item.ThumbnailPath.Substring(index);

                var tempItem = new EditPhotoDto() { Id = item.Id, ThumbnailPath = cutedPath, DateTimeUploading = item.DateTimeUploading, IsPublish = item.IsPublish, VoiceCount = item.VoiceCount };

                editPhotosList.Add(tempItem);
            }

            return editPhotosList;
        }

        public IEnumerable<string> GetPathsPublishPhoto()
        {
            var photos = _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDto>>(_unitOfwork.PhotoRepository.GetPublishPhotos());

            return photos.Select(p => p.PhotoPath);
        }

        /// <summary>
        /// Remove Photo entity
        /// </summary>
        /// <param name="model">Photo entity</param>
        public void Remove(PhotoDto model)
        {
            _unitOfwork.PhotoRepository.RemoveAsync(_mapper.Map<PhotoDto, Photo>(model));
            _unitOfwork.SaveASync();
        }

        public async Task<PhotoDto> ChangeVisibilityAsync(string Id)
        {
            var photo = await _unitOfwork.PhotoRepository.FindByIdAsync(Id);

            var photoDto = _mapper.Map<Photo, PhotoDto>(photo);

            photo.IsPublish = !photo.IsPublish;

            await _unitOfwork.PhotoRepository.UpdateAsync(photo);
            await _unitOfwork.SaveASync();

            return photoDto;
        }



        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _unitOfwork.Dispose();
            }
            disposed = true;
        }
    }
}
