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
                cfg.CreateMap<Photo, PhotoDto>().ReverseMap();
                cfg.CreateMap<PhotoDto, EditPhotoDto>().ReverseMap();
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

        public IEnumerable<EditPhotoDto> GelAllPhotosPaths(string id)
        {
            var photos = _unitOfwork.PhotoRepository.FindAll().Where(p => p.ApplicationUserId == id);

            var photoFullPath = _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDto>>(photos);

            var editPhotosList = new List<EditPhotoDto>();

            foreach (var item in photoFullPath)
            {
                var index = item.PhotoPath.IndexOf("Upload");
                var cutedPath = item.PhotoPath.Substring(index + 7);


                var tempItem = new EditPhotoDto() { Id = item.Id, PhotoPath = cutedPath, DateTimeUploading = item.DateTimeUploading, IsPublish = item.IsPublish };

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
