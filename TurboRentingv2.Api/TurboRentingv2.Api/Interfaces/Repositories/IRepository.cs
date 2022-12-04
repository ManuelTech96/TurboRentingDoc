using System.Linq.Expressions;

namespace TurboRentingv2.Api.Interfaces.Repositories
{
    public interface IRepository<TKey, TEntity> where TEntity : IAggregateRoot<TKey>
    {
        IQueryable<TEntity> Get();

        IQueryable<TEntity> Get(int? id);

        TEntity? Get(TKey? id, params Expression<Func<TEntity, IEnumerable<object>>>[] includes);

        IQueryable<TEntity> Where();

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Add(ICollection<TEntity> entities);

        void Update(TEntity? entity);

        void Update(ICollection<TEntity> entities);

        void Delete(TKey id);
    }
}
