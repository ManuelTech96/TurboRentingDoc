using AutoMapper;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Requests.Users;

namespace TurboRentingv2.Api.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, UserDto>();
            CreateMap<UpdateUserRequest, UserDto>();
        }
    }
}
