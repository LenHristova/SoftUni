namespace MiniORM
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    internal class ChangeTracker<TEntity>
    where TEntity : class, new()
    {
        private readonly List<TEntity> allEntities;

        private readonly List<TEntity> addedEntities;

        private readonly List<TEntity> removedEntities;

        public ChangeTracker(IEnumerable<TEntity> entities)
        {
            this.addedEntities = new List<TEntity>();
            this.removedEntities = new List<TEntity>();

            this.allEntities = CloneEntities(entities).ToList();
        }

        public IReadOnlyCollection<TEntity> AllEntities => this.allEntities.AsReadOnly();

        public IReadOnlyCollection<TEntity> AddedEntities => this.addedEntities.AsReadOnly();

        public IReadOnlyCollection<TEntity> RemovedEntities => this.removedEntities.AsReadOnly();

        private static IEnumerable<TEntity> CloneEntities(IEnumerable<TEntity> entities)
        {
            var clonedEntities = new List<TEntity>();
            var propertiesToClone = typeof(TEntity)
                .GetProperties()
                .Where(pi => pi.IsAllowedSqlType())
                .ToArray();

            foreach (var entity in entities)
            {
                var clonedEntity = new TEntity();

                foreach (var property in propertiesToClone)
                {
                    var originalValue = property.GetValue(entity);
                    property.SetValue(clonedEntity, originalValue);
                }

                clonedEntities.Add(clonedEntity);
            }

            return clonedEntities;
        }

        public void Add(TEntity item) => this.addedEntities.Add(item);

        public void Remove(TEntity item) => this.removedEntities.Add(item);

        public IEnumerable<TEntity> GetModifiedEntities(DbSet<TEntity> dbSet)
        {
            var modifiedEntities = new List<TEntity>();

            var primaryKeys = typeof(TEntity)
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (var proxyEntity in this.AllEntities)
            {
                var primaryKeyValues = GetPrimaryKeyValues(primaryKeys, proxyEntity).ToArray();

                var entity = dbSet.Entities
                    .SingleOrDefault(e => GetPrimaryKeyValues(primaryKeys, e)
                                    .SequenceEqual(primaryKeyValues));

                if (entity != null && IsModified(proxyEntity, entity))
                {
                    modifiedEntities.Add(entity);
                }
            }

            return modifiedEntities;
        }

        private static bool IsModified(TEntity proxyEntity, TEntity entity)
        {
            var modifiedProperties = typeof(TEntity)
                .GetProperties()
                .Where(pi => pi.IsAllowedSqlType() &&
                             !Equals(pi.GetValue(entity), pi.GetValue(proxyEntity)))
                .ToArray();

            var isModified = modifiedProperties.Any();
            return isModified;
        }

        private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, TEntity entity) =>
            primaryKeys.Select(pk => pk.GetValue(entity));
    }
}