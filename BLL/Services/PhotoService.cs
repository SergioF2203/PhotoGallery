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
            _unitOfwork.PhotoRepository.AddAsync(_mapper.Map<PhotoDto, Photo>(model));
            await _unitOfwork.SaveASync();
        }

        public async Task<PhotoDto> GetPhotoByIdAsync(string id)
        {
            var photo = await _unitOfwork.PhotoRepository.FindByIdAsync(id);

            return _mapper.Map<Photo, PhotoDto>(photo);
        }

        public IEnumerable<string> GelAllPhotosPaths()
        {
            var photos = _unitOfwork.PhotoRepository.FindAll().ToList();

            return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDto>>(photos).Select(p=>p.PhotoPath);
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
