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
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Photo, PhotoDto>().ReverseMap()));
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

        public IEnumerable<string> GelAllPhotosPaths(string id)
        {
            var photos = _unitOfwork.PhotoRepository.FindAll().Where(p=>p.ApplicationUserId == id);

            var photoFullPath =  _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDto>>(photos).Select(p=>p.PhotoPath);

            var shortPath = new List<string>();
            foreach(var item in photoFullPath)
            {
                var index = item.IndexOf("Upload");
                var temp = item.Substring(index + 7);
                shortPath.Add(temp);
            }

            return shortPath;
        }

        public IEnumerable<string> GetPathsPublishPhoto()
        {
            var photos = _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDto>>(_unitOfwork.PhotoRepository.GetPublishPhotos());

            return photos.Select(p => p.PhotoPath);
        }



        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed && disposing)
            {
                _unitOfwork.Dispose();
            }
            disposed = true;
        }
    }
}
