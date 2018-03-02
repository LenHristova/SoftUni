using System.Collections.Generic;
using P09_CollectionHierarchy.Contracts;

namespace P09_CollectionHierarchy.Models
{
    public class MyList<T>:IAddable<T>, IRemovable<T>, ICountable
    {
        private readonly Stack<T> _collection;

        public int Count { get; private set; }

        public MyList()
        {
            _collection = new Stack<T>();
            Count = 0;
        }

        public int Add(T item)
        {
            _collection.Push(item);
            Count++;
            return 0;
        }

        public T Remove()
        {
            Count--;
            return _collection.Pop();
        }
    }
}