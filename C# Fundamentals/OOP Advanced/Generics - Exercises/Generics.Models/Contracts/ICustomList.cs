using System.Collections.Generic;

public interface ICustomList<T> : IEnumerable<T>
{
    int Count { get; }

    int Capacity { get; }

    void Add(T element);

    T Remove(int index);

    T this[int index] { get; set; }

    bool Contains(T element);

    void Swap(int index1, int index2);

    int CountGreaterThan(T element);

    T Max();

    T Min();

    void Sort(IComparer<T> comparer = null);
}