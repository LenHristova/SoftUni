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
        private const int DEFAULT_CAPACITY = 16;
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

        public SimpleSortedList(IComparer<T> comparer) : this(comparer, DEFAULT_CAPACITY)
        {
        }

        public SimpleSortedList() : this(DefaultComparer(), DEFAULT_CAPACITY)
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

        public int Capacity => _innerCollection.Length;

        public int Size { get; private set; }

        public T this[int index]
        {
            get
            {
                if (index<0 || index >= Size)
                {
                    throw new IndexOutOfRangeException();
                }

                return _innerCollection[index];
            }
        }

        public void Add(T element)
        {
            EnsureNotNull(element);

            EnsureEnoughCapacity(Size);

            _innerCollection[Size] = element;
            Size++;

            SortInnerCollection();
        }

        private void SortInnerCollection() => Array.Sort(_innerCollection, 0, Size, _comparer);

        public void AddAll(ICollection<T> collection)
        {
            EnsureNotNull(collection);

            EnsureEnoughCapacity(Size + collection.Count);

            foreach (var element in collection)
            {
                _innerCollection[Size] = element;
                Size++;
            }

            SortInnerCollection();
        }

        private void EnsureEnoughCapacity(int neededCapacity)
        {
            if (neededCapacity >= _innerCollection.Length)
            {
                var newCapacity = _innerCollection.Length * 2;
                while (neededCapacity >= newCapacity)
                {
                    newCapacity *= 2;
                }

                Array.Resize(ref _innerCollection, newCapacity);
            }
        }

        public bool Remove(T element)
        {
            EnsureNotNull(element);

            for (int index = 0; index < Size; index++)
            {
                if (_innerCollection[index].Equals(element))
                {
                    RemoveAt(index);
                    return true;
                }
            }

            return false;
        }

        private void RemoveAt(int index)
        {
            Size--;

            for (int j = index; j < Size; j++)
            {
                _innerCollection[j] = _innerCollection[j + 1];
            }

            _innerCollection[Size] = default(T);

            EnsureCapacityNotTooBig();
        }

        private void EnsureCapacityNotTooBig()
        {
            if (Size < _innerCollection.Length / 2
                && Capacity > DEFAULT_CAPACITY)
            {
                Array.Resize(ref _innerCollection, _innerCollection.Length / 2);
            }
        }

        public string JoinWith(string joiner)
        {
            EnsureNotNull(joiner);

            var sb = new StringBuilder();

            foreach (var element in this)
            {
                sb.Append(element);
                sb.Append(joiner);
            }

            sb.Remove(sb.Length - joiner.Length, joiner.Length);
            return sb.ToString();
        }

        private static void EnsureNotNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
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
