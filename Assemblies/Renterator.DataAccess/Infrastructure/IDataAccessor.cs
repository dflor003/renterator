using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Renterator.DataAccess.Model;

namespace Renterator.DataAccess.Infrastructure
{
    public interface IDataAccessor : IDisposable
    {
        IQueryable<User> Users { get; }

        IQueryable<Role> Roles { get; }

        int SaveChanges();

        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        TEntity GetById<TEntity>(params object[] ids)
            where TEntity : class;

        IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class;

        TEntity Create<TEntity>(TEntity entity)
            where TEntity : class;

        IEnumerable<TEntity> Create<TEntity>(params TEntity[] entities)
            where TEntity : class;

        void Update<TEntity>(TEntity entity)
            where TEntity : class;

        void Update<TEntity>(params TEntity[] entities)
            where TEntity : class;

        void Delete<TEntity>(params TEntity[] entities)
            where TEntity : class;

        void Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        void Attach<TEntity>(TEntity entity)
            where TEntity : class;
    }
}
