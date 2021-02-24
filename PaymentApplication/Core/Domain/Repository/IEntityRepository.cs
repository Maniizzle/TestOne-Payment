using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PaymentApplication.Core.Domain.Repository
{
   public interface IEntityRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate, string includeProperties = "");

        TEntity Add(TEntity entity);


        void Remove(TEntity entity);


        void Update(TEntity entity);

        int Save();
    }
}
