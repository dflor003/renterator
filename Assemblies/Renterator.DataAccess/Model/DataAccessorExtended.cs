using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Renterator.Common;
using Renterator.DataAccess.Infrastructure;

namespace Renterator.DataAccess.Model
{
    public partial class RenteratorDataAccessor : IDataAccessor
    {
        #region Constructors

        static RenteratorDataAccessor()
        {
            QueryableIncluderExtension.Includer = new DbIncluder();
        }

        #endregion

        #region Public methods

        IQueryable<User> IDataAccessor.Users
        {
            get { return Users; }
        }

        IQueryable<Role> IDataAccessor.Roles
        {
            get { return Roles; }
        }

        IQueryable<Account> IDataAccessor.Accounts
        {
            get { return Accounts; }
        }

        IQueryable<AccountEntry> IDataAccessor.AccountEntries
        {
            get { return AccountEntries; }
        }

        IQueryable<Bill> IDataAccessor.Bills
        {
            get { return Bills; }
        }

        IQueryable<BillType> IDataAccessor.BillTypes
        {
            get { return BillTypes; }
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return GetAll<TEntity>().Where(predicate);
        }

        public TEntity GetById<TEntity>(params object[] ids)
            where TEntity : class
        {
            return this.Set<TEntity>().Find(ids);
        }

        public IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public TEntity Create<TEntity>(TEntity entity)
            where TEntity : class
        {
            return this.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> Create<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            if (entities == null)
            {
                throw new ApplicationException("No entities to create");
            }

            return
                from entity in entities
                select Create(entity);
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            this.Entry(entity).State = EntityState.Modified;
        }

        public void Update<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            foreach (TEntity entity in entities)
            {
                Update(entity);
            }
        }

        public void Delete<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            if (entities == null)
            {
                throw new ApplicationException("No entities to delete.");
            }

            foreach (TEntity entity in entities)
            {
                this.Set<TEntity>().Remove(entity);
            }
        }

        public void Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            GetAll<TEntity>().Where(predicate).ForEach(entity => Delete(entity));
        }

        public void Attach<TEntity>(TEntity entity)
            where TEntity : class
        {
            this.Set<TEntity>().Attach(entity);
        }

        public override int SaveChanges()
        {
            // Trim string values before saving to the db
            TrimValuesBeforeSave(this.ChangeTracker.Entries());

            return base.SaveChanges();
        }

        #endregion

        #region NonPublic methods

        private static void TrimValuesBeforeSave(IEnumerable<DbEntityEntry> entries)
        {
            // Get added/modified entities
            IEnumerable<DbEntityEntry> modifiedEntities =
                from entity in entries
                where entity.State == EntityState.Modified || entity.State == EntityState.Added
                select entity;

            // Iterate over every string property that has a value and trim it
            foreach (DbEntityEntry entry in modifiedEntities)
            {
                foreach (string propName in entry.CurrentValues.PropertyNames)
                {
                    string propValueString = entry.CurrentValues[propName] as string;
                    if (propValueString == null)
                    {
                        continue;
                    }

                    entry.CurrentValues[propName] = propValueString.Trim();
                }
            }
        }

        private class DbIncluder : IIncluder
        {
            public IQueryable<T> Include<T, TProperty>(IQueryable<T> source, Expression<Func<T, TProperty>> path)
                where T : class
            {
                return QueryableExtensions.Include(source, path);
            }
        }

        #endregion
    }
}
