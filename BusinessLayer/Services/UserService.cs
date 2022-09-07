using AutoMapper;
using CoreLayer.Dto;
using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using CoreLayer.UnitOfWork;

namespace BusinessLayer.Services
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> UserAdd()
        {
          var user =  await _userRepository.UserAddAsync();
          var userDto=_mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
