using AutoMapper;
using ENTOBEL_AURAVINA_API.Domains.Models;
using ENTOBEL_AURAVINA_API.Domains;
using ENTOBEL_AURAVINA_API.Domains.Services;
using ENTOBEL_AURAVINA_API.Resources;
using Microsoft.AspNetCore.Identity;
using System;
using ENTOBEL_AURAVINA_API.Domains.Repositories;

namespace ENTOBEL_AURAVINA_API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserAccount(AddUserViewModel userViewModel)
        {
            User user = new User(userViewModel.Name, userViewModel.UserName, userViewModel.Password);
            _userRepository.CreateUser(user);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _userRepository.GetAllUserAsync();
        }
    }
}
