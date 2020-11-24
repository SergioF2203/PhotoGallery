using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public PhotoService(IUnitOfWork unitOfWork/*, Mapper mapper*/)
        {
            _unitOfwork = unitOfWork;
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Photo, PhotoDto>().ReverseMap()));
            //_mapper = mapper;
        }
        public async Task AddAsync(PhotoDto model)
        {
            await _unitOfwork.PhotoRepository.AddAsync(_mapper.Map<PhotoDto, Photo>(model));
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
