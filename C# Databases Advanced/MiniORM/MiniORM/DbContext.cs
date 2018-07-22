namespace MiniORM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;

    public abstract class DbContext
    {
        private readonly DatabaseConnection connection;

        private readonly IDictionary<Type, PropertyInfo> dbSetProperties;

        protected DbContext(string connectionString)
        {
            this.connection = new DatabaseConnection(connectionString);

            this.dbSetProperties = DiscoverDbSets();

            using (new ConnectionManager(connection))
            {
                this.InitializeDbSets();
            }

            this.MapAllRelations();
        }

        private IDictionary<Type, PropertyInfo> DiscoverDbSets()
        {
            var dbSets = this.GetType()
                .GetProperties()
                .Where(pi => pi.PropertyType.IsGenericType &&
                             pi.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToDictionary(pi => pi.PropertyType.GetGenericArguments().First(), pi => pi);

            return dbSets;
        }

        private void InitializeDbSets()
        {
            foreach (var dbSet in this.dbSetProperties)
            {
                var dbSetType = dbSet.Key;
                var dbSetProperty = dbSet.Value;

                var populateDbSetMethod = typeof(DbContext).MakeGenericMethod(nameof(PopulateDbSet), dbSetType);
                populateDbSetMethod.Invoke(this, new object[] { dbSetProperty });
            }
        }

        private void PopulateDbSet<TEntity>(PropertyInfo dbSet)
            where TEntity : class, new()
        {
            var entities = LoadTableEntities<TEntity>();

            var dbSetInstance = new DbSet<TEntity>(entities);
            ReflectionHelper.ReplaceBackingField(this, dbSet.Name, dbSetInstance);
        }

        private IEnumerable<TEntity> LoadTableEntities<TEntity>()
            where TEntity : class, new()
        {
            var table = typeof(TEntity);
            var tableName = GetTableName(table);
            var columns = GetEntityColumnNames(table, tableName);

            var fetchedRows = this.connection
                .FetchResultSet<TEntity>(tableName, columns)
                .ToArray();

            return fetchedRows;
        }

        private string GetTableName(Type tableType)
        {
            var tableName = tableType.GetCustomAttribute<TableAttribute>()?.Name
                            ?? this.dbSetProperties[tableType].Name;

            return tableName;
        }

        private string[] GetEntityColumnNames(Type table, string tableName)
        {
            var dbColumns = this.connection.FetchColumnNames(tableName);

            var columns = table.GetProperties()
                .Where(pi =>
                    dbColumns.Contains(pi.Name) &&
                    !pi.HasAttribute<NotMappedAttribute>() &&
                    pi.IsAllowedSqlType())
                .Select(pi => pi.Name)
                .ToArray();

            return columns;
        }

        private void MapAllRelations()
        {
            foreach (var dbSetProperty in this.dbSetProperties)
            {
                var dbSetType = dbSetProperty.Key;
                var dbSet = dbSetProperty.Value.GetValue(this);

                var mapRelationsGeneric = typeof(DbContext).MakeGenericMethod(nameof(MapRelations), dbSetType);
                mapRelationsGeneric.Invoke(this, new object[] { dbSet });
            }
        }

        private void MapRelations<TEntity>(DbSet<TEntity> dbSet)
        where TEntity : class, new()
        {
            MapNavigationProperties(dbSet);

            MapCollectionProperties(dbSet);
        }

        private void MapNavigationProperties<TEntity>(DbSet<TEntity> dbSet)
            where TEntity : class, new()
        {
            var entityType = typeof(TEntity);
            var foreignKeys = entityType.GetProperties()
                .Where(pi => pi.HasAttribute<ForeignKeyAttribute>())
                .ToArray();

            foreach (var foreignKey in foreignKeys)
            {
                var navigationPropertyName = foreignKey.GetCustomAttribute<ForeignKeyAttribute>().Name;

                var navigationProperty = entityType.GetProperty(navigationPropertyName);

                var navigationPropertyType = navigationProperty.PropertyType;

                var navigationDbSet = this.dbSetProperties[navigationPropertyType].GetValue(this);
                
                var navigationPrimaryKey = navigationPropertyType.GetProperties()
                    .First(pi => pi.HasAttribute<KeyAttribute>());

                foreach (var entity in dbSet)
                {
                    var foreignKeyValue = foreignKey.GetValue(entity);

                    var navigationPropertyValue = ((IEnumerable<object>)navigationDbSet)
                        .First(currentNavigationProperty =>
                            navigationPrimaryKey.GetValue(currentNavigationProperty).Equals(foreignKeyValue));

                    navigationProperty.SetValue(entity, navigationPropertyValue);
                }
            }
        }

        private void MapCollectionProperties<TEntity>(DbSet<TEntity> dbSet) 
            where TEntity : class, new()
        {
            var entityType = typeof(TEntity);

            var collections = entityType
                .GetProperties()
                .Where(pi =>
                    pi.PropertyType.IsGenericType &&
                    pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                .ToArray();

            foreach (var collection in collections)
            {
                var collectionType = collection.PropertyType.GenericTypeArguments.First();

                var mapCollectionMethod = typeof(DbContext).MakeGenericMethod(nameof(MapCollection), entityType, collectionType);

                mapCollectionMethod.Invoke(this, new object[] { dbSet, collection });
            }
        }

        private void MapCollection<TDbSet, TCollection>(DbSet<TDbSet> dbSet, PropertyInfo collentionProperty)
            where TDbSet : class, new()
            where TCollection : class, new()
        {
            var entityType = typeof(TDbSet);
            var collectionType = typeof(TCollection);

            var collectionTypePrimaryKeys = collectionType
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            var primaryKey = collectionTypePrimaryKeys.First();
            var foreignKey = entityType.GetProperties()
                .First(pi => pi.HasAttribute<KeyAttribute>());

            var isManyToMany = collectionTypePrimaryKeys.Length >= 2;
            if (isManyToMany)
            {
                primaryKey = collectionType.GetProperties()
                    .First(pi => pi.HasAttribute<ForeignKeyAttribute>() &&
                                 collectionType
                                     .GetProperty(pi.GetCustomAttribute<ForeignKeyAttribute>().Name)
                                     .PropertyType == entityType);
            }

            var navigationDbSet = (DbSet<TCollection>)this.dbSetProperties[collectionType].GetValue(this);

            foreach (var entity in dbSet)
            {
                var primaryKeyValue = foreignKey.GetValue(entity);

                var navigationEntities = navigationDbSet
                    .Where(navigationEntity => primaryKey.GetValue(navigationEntity).Equals(primaryKeyValue))
                    .ToArray();

                ReflectionHelper.ReplaceBackingField(entity, collentionProperty.Name, navigationEntities);
            }
        }

        public void SaveChanges()
        {
            var dbSets = this.dbSetProperties
                .Select(pi => pi.Value.GetValue(this))
                .ToArray();

            foreach (IEnumerable<object> dbSet in dbSets)
            {
                var invalidEntities = dbSet
                    .Where(e => !IsObjectValid(e))
                    .ToArray();

                if (invalidEntities.Any())
                {
                    throw new InvalidOperationException($"{invalidEntities.Length} invalid entities found in {dbSet.GetType().Name}!");
                }
            }

            using (new ConnectionManager(this.connection))
            {
                using (var transaction = this.connection.StartTransaction())
                {
                    foreach (IEnumerable dbSet in dbSets)
                    {
                        var dbSetType = dbSet.GetType().GetGenericArguments().First();

                        var persistMethod = typeof(DbContext).MakeGenericMethod(nameof(Persist), dbSetType);

                        try
                        {
                            persistMethod.Invoke(this, new object[] { dbSet });
                            
                        }
                        catch (TargetInvocationException tie)
                        {
                            throw tie.InnerException;
                        }
                        catch (InvalidOperationException)
                        {
                            transaction.Rollback();
                            throw;
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine($"{e.Class} {e.ClientConnectionId} {e.Source} {e.HResult} {e.Data}");
                            transaction.Rollback();
                            throw;
                        }
                    }

                    transaction.Commit();
                }
            }
        }

        private static bool IsObjectValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationErrors = new List<ValidationResult>();

            var validationResult = Validator.TryValidateObject(entity, validationContext, validationErrors, validateAllProperties: true);

            return validationResult;
        }

        private void Persist<TEntity>(DbSet<TEntity> dbSet)
        where TEntity : class, new()
        {
            var tableName = this.GetTableName(typeof(TEntity));

            var columns = this.connection
                .FetchColumnNames(tableName)
                .ToArray();

            var hasPersisted = false;
            if (dbSet.ChangeTracker.AddedEntities.Any())
            {
                this.connection
                    .InsertEntities(dbSet.ChangeTracker.AddedEntities, tableName, columns);
                hasPersisted = true;
            }

            var modifiedEntities = dbSet.ChangeTracker
                .GetModifiedEntities(dbSet)
                .ToArray();

            if (modifiedEntities.Any())
            {
                this.connection
                    .UpdateEntities(modifiedEntities, tableName, columns);
                hasPersisted = true;
            }

            if (dbSet.ChangeTracker.RemovedEntities.Any())
            {
                this.connection
                    .DeleteEntities(dbSet.ChangeTracker.RemovedEntities, tableName, columns);
                hasPersisted = true;
            }

            if (hasPersisted)
            {
                dbSet.RefreshChangeTracker();
            }
        }

    }
}