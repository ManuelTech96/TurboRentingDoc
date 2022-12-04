using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TurboRentingv2.Api.Interfaces.Repositories;
using TurboRentingv2.Api.Interfaces;
using TurboRentingv2.Api.Context;
using TurboRentingv2.Api.Exceptions;

namespace TurboRentingv2.Api.Models.Repositories
{
    public class Repository<TKey, TAggregateRoot> :IRepository<TKey, TAggregateRoot> where TAggregateRoot : class, IAggregateRoot<TKey>
    {
        private readonly DbContext context;

        private readonly DbSet<TAggregateRoot> entitySet;

        public Repository(TurboRentingContext context)
        {
            this.context = context;

            entitySet = context.Set<TAggregateRoot>();
        }

        public IQueryable<TAggregateRoot>? Get()
        {
            var entities = entitySet.AsNoTracking();

            return entities;
        }

        public IQueryable<TAggregateRoot>? Get(int? id)
        {
            var entities = entitySet.AsNoTracking();

            return entities;
        }

        public TAggregateRoot? Get(TKey? id, params Expression<Func<TAggregateRoot, IEnumerable<object>>>[] includes)
        {
            var entity = entitySet.Find(id);

            if (entity != default)
            {
                foreach (var include in includes)
                    context.Entry(entity).Collection(include).Load();
            }

            return entity;
        }

        public IQueryable<TAggregateRoot> Where()
        {
            var query = context.Set<TAggregateRoot>().AsQueryable();

            return query;
        }


        public IQueryable<TAggregateRoot> Where(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            var query = context.Set<TAggregateRoot>().Where(predicate);

            return query;
        }
        public void Add(TAggregateRoot entity)
        {
            context.Set<TAggregateRoot>().Add(entity);

            context.SaveChanges();
        }


        public void Add(ICollection<TAggregateRoot> entities)
        {
            if (entities.Count > 0)
            {
                context.Set<TAggregateRoot>().AddRange(entities);

                context.SaveChanges();
            }
        }


        public void Update(TAggregateRoot? entity)
        {
            if (entity != default)
            {
                context.Entry(entity).State = EntityState.Modified;
                context.Set<TAggregateRoot>().Update(entity);
                context.SaveChanges();
            }
        }


        public void Update(ICollection<TAggregateRoot> entities)
        {
            if (entities.Count > 0)
            {
                context.Set<TAggregateRoot>().UpdateRange(entities);

                context.SaveChanges();
            }
        }

        public void Delete(TKey id)
        {
            var model = Get(id);

            if (model == null)
                throw new EntityNotFoundException(id, typeof(TAggregateRoot));

            entitySet.Remove(model);

            context.SaveChanges();
        }
    }
}
