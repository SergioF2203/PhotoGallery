using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        /// <summary>
        /// Ctor Album Service
        /// </summary>
        /// <param name="unitOfWork">interface IUnitOfWork</param>
        public AlbumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Album, AlbumDto>().ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.AlbumName)).ReverseMap()));
        }

        /// <summary>
        /// Add album model
        /// </summary>
        /// <param name="model">Album model</param>
        public void Add(AlbumDto model)
        {
            _unitOfWork.AlbumRepository.Add(_mapper.Map<AlbumDto, Album>(model));
            _unitOfWork.SaveASync();
        }

        /// <summary>
        /// Remove Album
        /// </summary>
        /// <param name="model">Album model</param>
        public void Remove(AlbumDto model)
        {
            _unitOfWork.AlbumRepository.RemoveAsync(_mapper.Map<AlbumDto, Album>(model));
            _unitOfWork.SaveASync();
        }

        /// <summary>
        /// Get Album by Id
        /// </summary>
        /// <param name="albumId">Album's Id</param>
        /// <returns>Album DTO instance</returns>
        public async Task<AlbumDto> GetAlbumByIdAsync(string albumId)
        {
            var album = await _unitOfWork.AlbumRepository.FindByIdAsync(albumId);

            return _mapper.Map<Album, AlbumDto>(album);
        }

        /// <summary>
        /// Get All Albums Dto instance
        /// </summary>
        /// <returns>IEnumerable Album's Dto</returns>
        public IEnumerable<AlbumDto> GetAlbums()
        {
            return _mapper.Map<IEnumerable<Album>, IEnumerable<AlbumDto>>(_unitOfWork.AlbumRepository.FindAll());
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
                _unitOfWork.Dispose();

            disposed = true;
        }
    }
}
