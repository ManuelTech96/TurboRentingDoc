using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Interfaces.Services
{
    public interface IUserService
    {
        //READ
        ICollection<UserDto> Get();
        UserDto GetById(int id);

        //CREATE
        UserDto Add(UserDto user);

        //UPDATE
        void Update(UserDto user);

        //DELETE
        void Delete(int id);
    }
}
