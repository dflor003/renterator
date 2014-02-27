using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Renterator.Common;
using Renterator.DataAccess.Infrastructure;
using Renterator.DataAccess.Model;

namespace Renterator.Services.Tests.Helpers
{
    public class InMemoryDataAccessor : IDataAccessor
    {
        private static readonly MethodInfo CreateMethod;
        private static readonly MethodInfo UpdateMethod;

        static InMemoryDataAccessor()
        {
            MethodInfo[] allMethods = typeof(InMemoryDataAccessor).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            CreateMethod = allMethods.FirstOrDefault(method => method.Name == "Create" && !method.GetParameters().First().ParameterType.IsArray);
            UpdateMethod = allMethods.FirstOrDefault(method => method.Name == "Update" && !method.GetParameters().First().ParameterType.IsArray);
        }

        public InMemoryDataAccessor()
        {
            Committed = new HashSet<object>();
            PendingActions = new List<Action>();
        }

        public IQueryable<User> Users
        {
            get { return GetAll<User>(); }
        }

        public IQueryable<Role> Roles
        {
            get { return GetAll<Role>(); }
        }

        public IQueryable<Account> Accounts
        {
            get { return GetAll<Account>(); }
        }

        public IQueryable<AccountEntry> AccountEntries
        {
            get { return GetAll<AccountEntry>(); }
        }

        public IQueryable<Bill> Bills
        {
            get { return GetAll<Bill>(); }
        }

        public IQueryable<BillType> BillTypes
        {
            get { return GetAll<BillType>(); }
        }

        public HashSet<object> Committed { get; private set; }

        public List<Action> PendingActions { get; private set; }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class
        {
            return GetAll<TEntity>().Where(predicate);
        }

        public TEntity GetById<TEntity>(params object[] ids)
            where TEntity : class
        {
            return GetAll<TEntity>().SingleOrDefault(ent => EntityHelper.AreKeysEqual(ent, ids));
        }

        public IQueryable<TEntity> GetAll<TEntity>()
            where TEntity : class
        {
            return Committed.OfType<TEntity>().AsQueryable();
        }

        public TEntity Create<TEntity>(TEntity entity)
            where TEntity : class
        {
            PendingActions.Add(() =>
            {
                Committed.Add(entity);

                if (EntityHelper.IsIdentityType(typeof(TEntity)))
                {
                    int maxKey = GetAll<TEntity>().Max(ent => EntityHelper.GetKeys(ent).OfType<int>().First());
                    EntityHelper.SetKeys(entity, maxKey + 1);
                }
            });

            UpdateAssociations(entity);

            return entity;
        }

        public IEnumerable<TEntity> Create<TEntity>(params TEntity[] entities)
            where TEntity : class
        {
            if (entities == null)
            {
                throw new ApplicationException("InMemoryDataAccessor: Passed null to create entities");
            }

            return
                from entity in entities
                select Create(entity);
        }

        public void Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            TEntity updated = entity;
            TEntity original = GetAll<TEntity>().Single(item => EntityHelper.AreKeysEqual(entity, item));
            PendingActions.Add(() => EntityHelper.SetValueProperties(updated, original));
            UpdateAssociations(entity);
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
                throw new ApplicationException("InMemoryDataAccesor: Passed null to delete");
            }

            foreach (TEntity entity in entities)
            {
                TEntity current = entity;
                if (!Committed.Contains(current))
                {
                    throw new ApplicationException("InMemoryDataAccesor: Trying to delete entity that is not in the database");
                }

                PendingActions.Add(() => Committed.Remove(current));
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
        }

        public int SaveChanges()
        {
            int changeCount = this.PendingActions.Count;
            this.PendingActions.ForEach(action => action());
            this.PendingActions.Clear();
            return changeCount;
        }

        public void Dispose()
        {
        }

        private void UpdateAssociations<TEntity>(TEntity entity)
        {
            foreach (PropertyInfo associationProp in EntityHelper.AssociationPropertyMap[typeof(TEntity)])
            {
                IEnumerable values = (IEnumerable)associationProp.GetValue(entity);
                foreach (object value in values)
                {
                    Type objType = value.GetType();
                    object original = Committed.FirstOrDefault(obj => obj.GetType() == value.GetType() && EntityHelper.AreKeysEqual(obj, value));
                    if (original == null)
                    {
                        CreateMethod.MakeGenericMethod(objType).Invoke(this, new[] { value });
                    }
                    else
                    {
                        UpdateMethod.MakeGenericMethod(objType).Invoke(this, new[] { value });
                    }
                }
            }
        }

        private static class EntityHelper
        {
            public static readonly Type[] TypesWithIdentity = new[] { typeof(User) };
            public static readonly Dictionary<Type, PropertyInfo[]> IdPropertyMap = new Dictionary<Type, PropertyInfo[]>();
            public static readonly Dictionary<Type, PropertyInfo[]> ValuePropertyMap;
            public static readonly Dictionary<Type, PropertyInfo[]> AssociationPropertyMap;

            static EntityHelper()
            {
                // Setup
                const BindingFlags PropertyTypes = BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty | BindingFlags.GetProperty;
                Type userType = typeof(User);
                Type[] typesToCheck = new[] { userType };

                // Split up value type and collection type properties
                var typeToProperties =
                    (from type in typesToCheck
                     select new { Type = type, Properties = type.GetProperties(PropertyTypes) }).ToArray();
                var valueProps =
                    from entry in typeToProperties
                    select new
                    {
                        entry.Type,
                        Properties =
                            (from prop in entry.Properties
                             where !prop.PropertyType.Name.StartsWith("ICollection")
                             select prop).ToArray()
                    };
                var associationProps =
                    from entry in typeToProperties
                    select new
                    {
                        entry.Type,
                        Properties =
                            (from prop in entry.Properties
                             where prop.PropertyType.Name.StartsWith("ICollection")
                             select prop).ToArray()
                    };

                // Re map them
                ValuePropertyMap = valueProps.ToDictionary(x => x.Type, x => x.Properties);
                AssociationPropertyMap = associationProps.ToDictionary(x => x.Type, x => x.Properties);

                // Map of primary key properties
                IdPropertyMap[typeof(User)] = new[] { userType.GetProperty("Id") };
            }

            public static bool AreKeysEqual<TEntity>(TEntity entity, IEnumerable<object> otherKeys)
            {
                IEnumerable<object> entityKeys = GetKeys(entity);
                return entityKeys.SequenceEqual(otherKeys);
            }

            public static bool AreKeysEqual<TEntity>(TEntity first, TEntity second)
            {
                return AreKeysEqual(first, GetKeys(second));
            }

            public static void SetValueProperties<TEntity>(TEntity updatedEntity, TEntity originalEntity)
            {
                PropertyInfo[] valueProps = ValuePropertyMap[typeof(TEntity)];
                foreach (PropertyInfo prop in valueProps)
                {
                    object newValue = prop.GetValue(updatedEntity);
                    prop.SetValue(originalEntity, newValue);
                }
            }

            public static void SetKeys<TEntity>(TEntity entity, params object[] keys)
            {
                PropertyInfo[] keyProps = IdPropertyMap[entity.GetType()];
                if (keyProps.Length != keys.Length)
                {
                    throw new ApplicationException("Keys arrays must match number of keys");
                }

                for (int i = 0; i < keyProps.Length; i++)
                {
                    PropertyInfo prop = keyProps[i];
                    object value = keys[i];

                    prop.SetValue(entity, value);
                }
            }

            public static IEnumerable<object> GetKeys<TEntity>(TEntity entity)
            {
                PropertyInfo[] keyProps = IdPropertyMap[entity.GetType()];
                return
                    from prop in keyProps
                    select prop.GetValue(entity);
            }

            public static bool IsIdentityType(Type entityType)
            {
                return TypesWithIdentity.Contains(entityType);
            }
        }
    }
}
