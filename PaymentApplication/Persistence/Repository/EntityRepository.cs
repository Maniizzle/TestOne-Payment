using Microsoft.EntityFrameworkCore;
using PaymentApplication.Domain.Repository;
using PaymentApplication.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentApplication.Persistence.Repository
{
    

    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
    {
        protected readonly PaymentAppDbContext context;
        private DbSet<TEntity> _dbSet;

        public EntityRepository(PaymentAppDbContext context)
        {
            this.context = context;
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_dbSet == null)
                {
                    _dbSet = context.Set<TEntity>();
                }
                return _dbSet;
            }
        }

        public TEntity Add(TEntity entity)
        {
            var entitysaved = Entities.Add(entity).Entity;
            return entitysaved;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public int Count()
        {
            return Entities.Count();
        }

        public IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, string includeProperties = "")
        {
            IQueryable<TEntity> query = Entities;
            if (!includeProperties.Equals(string.Empty))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.Where(predicate).AsEnumerable();
        }

        public TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities;
        }

        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public bool Exist(int id)
        {
            return Entities.Any();
        }
    }
}
