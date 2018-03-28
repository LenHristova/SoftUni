using System;
using System.Collections;
using System.Collections.Generic;

namespace P03_Stack
{
    public class Stack<T> : IEnumerable<T>
    {
        private T[] _items;

        public Stack(params T[] items)
        {
            _items = items;
            Count = _items.Length;
        }

        public int Count { get; private set; }

        public void Push(T item)
        {
            if (Count == _items.Length)
            {
                Array.Resize(ref _items, (_items.Length + 1) * 2);
            }

            _items[Count] = item;
            Count++;
        }

        public void Pop()
        {
            if (Count == 0)
            {
                throw new IndexOutOfRangeException("No elements");
            }

            Count--;
            _items[Count] = default(T);

            if (Count < _items.Length / 4)
            {
                Array.Resize(ref _items, _items.Length / 2);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int index = Count - 1; index >= 0; index--)
            {
                yield return _items[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}