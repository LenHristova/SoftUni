using System.Collections.Generic;
using P09_CollectionHierarchy.Contracts;

namespace P09_CollectionHierarchy.Models
{
    public class AddCollection<T> : IAddable<T>
    {
        private readonly ICollection<T> _collection;

        public AddCollection()
        {
            _collection = new List<T>();
        }

        public int Add(T item)
        {
            _collection.Add(item);
            return _collection.Count - 1;
        }
    }
}