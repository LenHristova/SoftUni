using System;
using System.Collections.Generic;
using System.Linq;

namespace P03_Iterator
{
    public class ListIterator
    {
        private readonly List<string> _elements;
        private int _internalIndex;

        public ListIterator(params string[] elements)
        {
            EnsureNotNullCollection(elements);

            _elements = new List<string>(elements);
            _internalIndex = 0;
        }

        private static void EnsureNotNullCollection(params string[] elements)
        {
            if (elements == null)
            {
                throw new ArgumentException("Collection can not be null.");
            }
        }

        public bool Move()
        {
            if (HasNext())
            {
                _internalIndex++;
                return true;
            }

            return false;
        }

        public bool HasNext() => _internalIndex < _elements.Count;

        public string Print()
        {
            if (!_elements.Any())
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

           return _elements[_internalIndex];
        }
    }
}
