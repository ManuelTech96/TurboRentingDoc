using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Interfaces.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int id);
        ICollection<User>? Get();
        User? GetById(int id);
        void Update(User user);

    }
}
