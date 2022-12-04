using TurboRentingv2.Api.Exceptions;
using TurboRentingv2.Api.Interfaces;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public UserDto Add(UserDto user)
        {
            var entity = mapper.Map<User>(user);

            userRepository.Add(entity);

            var result = mapper.Map<UserDto>(entity);

            return result;
        }

        public void Delete(int id) => userRepository.Delete(id);

        public UserDto GetById(int id)
        {
            var entity = userRepository.GetById(id);

            if (entity == default)
                throw new UserNotFoundException(id);

            var result = mapper.Map<UserDto>(entity);

            return result;
        }

        public ICollection<UserDto> Get()
        {
            var users = userRepository.Get();

            if (users == default)
                return default;

            var result = users.Select(mapper.Map<UserDto>)
                              .ToList();

            return result;
        }

        public void Update(UserDto user)
        {
            User? currentUser = userRepository.GetById(user.Id);

            if (currentUser == default)
                throw new UserNotFoundException(user.Id);

            User updatedUser = mapper.Map(user, currentUser);

            userRepository.Update(updatedUser);
        }
    }
}
