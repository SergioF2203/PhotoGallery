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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(UserModel model)
        {
            // need to check model

            await _unitOfWork.UserRepository.AddAsync(_mapper.Map<UserModel, User>(model));
            await _unitOfWork.SaveAsync();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAll()
        {
            var users = _unitOfWork.UserRepository.FindAll().ToList();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>().ForMember(dest=>dest.UserModelEmail, opt=>opt.MapFrom(src=>src.UserEmail))).CreateMapper();
            var userModels = mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(users);

            return userModels;
        }

        public Task<UserModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
