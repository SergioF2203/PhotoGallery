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
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AlbumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Album, AlbumDto>().ForMember(dst=>dst.Title, opt=>opt.MapFrom(src=>src.AlbumName)).ReverseMap()));
        }

        public void Add(AlbumDto model)
        {
            _unitOfWork.AlbumRepository.Add(_mapper.Map<AlbumDto, Album>(model));
            _unitOfWork.SaveASync();
        }

        public void Remove(AlbumDto model)
        {
            _unitOfWork.AlbumRepository.RemoveAsync(_mapper.Map<AlbumDto, Album>(model));
            _unitOfWork.SaveASync();
        }

        public async Task<AlbumDto> GetAlbumByIdAsync(string albumId)
        {
            var album = await _unitOfWork.AlbumRepository.FindByIdAsync(albumId);

            return _mapper.Map<Album, AlbumDto>(album);
        }

        public IEnumerable<AlbumDto> GetAlbums()
        {
            return _mapper.Map<IEnumerable<Album>, IEnumerable<AlbumDto>>(_unitOfWork.AlbumRepository.FindAll());
        }

        public async Task GetAlbumById(string albumId)
        {

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
