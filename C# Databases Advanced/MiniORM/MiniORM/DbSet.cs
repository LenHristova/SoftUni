namespace MiniORM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DbSet<TEntity> : ICollection<TEntity>
    where TEntity : class, new()
    {
        internal DbSet(IEnumerable<TEntity> entities)
        {
            this.Entities = entities.ToList();

            this.ChangeTracker = new ChangeTracker<TEntity>(entities);
        }

        internal ChangeTracker<TEntity> ChangeTracker { get; set; }

        internal IList<TEntity> Entities { get; set; }

        internal void RefreshChangeTracker() => this.ChangeTracker = new ChangeTracker<TEntity>(this.Entities);

        public void Add(TEntity item)
        {
            EnsureNotNull(item);

            this.Entities.Add(item);
            this.ChangeTracker.Add(item);
        }

        private static void EnsureNotNull(params TEntity[] item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            }
        }

        public void Clear()
        {
            foreach (var entity in this.Entities)
            {
                this.ChangeTracker.Remove(entity);
            }

            this.Entities.Clear();
        }

        public bool Contains(TEntity entity) => this.Entities.Contains(entity);

        public void CopyTo(TEntity[] array, int arrayIndex) => this.Entities.CopyTo(array, arrayIndex);

        public bool Remove(TEntity item)
        {
            EnsureNotNull(item);

            var isRemovedSuccessfully = this.Entities.Remove(item);
            if (isRemovedSuccessfully)
            {
                this.ChangeTracker.Remove(item);
            }

            return isRemovedSuccessfully;
        }

        public void RemoveRange(IEnumerable<TEntity > entities)
        {
            foreach (var entity in entities)
            {
                this.Remove(entity);
            }
        }

        public int Count => this.Entities.Count;

        public bool IsReadOnly => this.Entities.IsReadOnly;

        public IEnumerator<TEntity> GetEnumerator() => this.Entities.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}