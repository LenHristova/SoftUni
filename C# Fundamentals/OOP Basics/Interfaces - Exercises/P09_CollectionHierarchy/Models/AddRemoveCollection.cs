using System.Collections.Generic;
using P09_CollectionHierarchy.Contracts;

namespace P09_CollectionHierarchy.Models
{
    public class AddRemoveCollection<T> : IRemovable<T>, IAddable<T>
    {
        private readonly Queue<T> _collection;

        public AddRemoveCollection()
        {
            _collection = new Queue<T>();
        }

        public int Add(T item)
        {
            _collection.Enqueue(item);
            return 0;
        }

        public T Remove()
        {
            return _collection.Dequeue();
        }
    }
}