using System;
using System.Collections.Generic;

namespace BashSoft.Contracts
{
    public interface ISimpleOrderedBag<T> : IEnumerable<T>
    where T : IComparable<T>
    {
        int Capacity { get; }

        int Size { get; }

        T this[int index] { get; }

        void Add(T element);

        void AddAll(ICollection<T> collection);

        bool Remove(T element);

        string JoinWith(string joiner);
    }
}
