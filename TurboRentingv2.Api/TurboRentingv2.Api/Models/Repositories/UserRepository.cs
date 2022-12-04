using TurboRentingv2.Api.Exceptions;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Models.Entities;
using TurboRentingv2.Api.Models.Enums;

namespace TurboRentingv2.Api.Models.Repositories
{
    public class UserRepository : IUserRepository 
    {
        private readonly IRepository<int, User> repository;

        public UserRepository(IRepository<int, User> repository)
        {
            this.repository = repository;
        }   

        public void Add(User user) => repository.Add(user);

        public void Delete(int id)
        {
            var entity = repository.Where(e => e.Id == id).SingleOrDefault();

            if (entity == default)
                throw new UserNotFoundException(id);

            entity.Activo = false;

            repository.Update(entity);
        }

        public ICollection<User>? Get()
        {
            var entities = repository.Where(e => e.Activo);

            return entities.ToList();
        }

        public User? GetById(int id)
        {
            var entities = repository.Where(c => c.Id == id && c.Activo);

            return entities.SingleOrDefault();
        }

        public void Update(User user) => repository.Update(user);

        public ICollection<User>? GetByName(string name)
        {
            var entities = repository.Where(c => c.Name == name && c.Activo);
            return entities.ToList();
        }
    }
}
