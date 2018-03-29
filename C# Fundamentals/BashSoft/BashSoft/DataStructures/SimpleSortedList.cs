using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using BashSoft.Contracts;

namespace BashSoft.DataStructures
{
    public class SimpleSortedList<T> : ISimpleOrderedBag<T>
    where T : IComparable<T>
    {
        private const int DEFAULT_SIZE = 16;
        private static Comparer<T> DefaultComparer() => Comparer<T>.Create((x, y) => x.CompareTo(y));

        private T[] _innerCollection;
        private readonly IComparer<T> _comparer;

        public SimpleSortedList(IComparer<T> comparer, int capacity)
        {
            InicializeInnerCollection(capacity);
            _comparer = comparer;
            Size = 0;
        }

        public SimpleSortedList(int capacity) : this(DefaultComparer(), capacity)
        {
        }

        public SimpleSortedList(IComparer<T> comparer) : this(comparer, DEFAULT_SIZE)
        {
        }

        public SimpleSortedList() : this(DefaultComparer(), DEFAULT_SIZE)
        {
        }

        private void InicializeInnerCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity can not be negative");
            }

            _innerCollection = new T[capacity];
        }

        public int Size { get; private set; }


        public void Add(T element)
        {
            EnsureEnoughSize(Size);

            _innerCollection[Size] = element;
            Size++;

            SortInnerCollection();
        }

        private void SortInnerCollection() => Array.Sort(_innerCollection, 0, Size, _comparer);

        public void AddAll(ICollection<T> collection)
        {
            EnsureEnoughSize(Size + collection.Count);

            foreach (var element in collection)
            {
                _innerCollection[Size] = element;
                Size++;
            }

            SortInnerCollection();
        }

        private void EnsureEnoughSize(int neededSize)
        {
            if (neededSize >= _innerCollection.Length)
            {
                var newSize = _innerCollection.Length * 2;
                while (neededSize >= newSize)
                {
                    newSize *= 2;
                }

                Array.Resize(ref _innerCollection, newSize);
            }
        }

        public string JoinWith(string joiner)
        {
            var sb = new StringBuilder();

            foreach (var element in this)
            {
                sb.Append(element);
                sb.Append(joiner);
            }

            sb.Remove(sb.Length - 1, 1);
            //return sb.ToString().TrimEnd(joiner.ToCharArray());
            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Size; i++)
            {
                yield return _innerCollection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
