using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace P02_Collection
{
    public class ListyIterator<T> :IEnumerable<T>
        where T : IComparable<T>
    {
        private readonly IList<T> _items;
        private int _currentIndex;

        public ListyIterator(IEnumerable<T> items)
        {
            _items = new List<T>(items);
            _currentIndex = 0;
        }

        private T Current => _items[_currentIndex];

        public bool Move()
        {
            if (!HasNext())
            {
                return false;
            }

            _currentIndex++;
            return true;
        }

        public bool HasNext()
        {
            return _currentIndex + 1 < _items.Count;
        }

        public void Print()
        {
            if (!_items.Any())
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            Console.WriteLine(Current);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}