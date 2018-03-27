using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomList<T> : ICustomList<T>
where T : IComparable<T>
{
    private const int DEFAULT_CAPACITY = 4;
    private T[] elements;

    private CustomList(T[] array)
    {
        this.elements = array;
        this.Count = 0;
    }

    public CustomList() : this(new T[DEFAULT_CAPACITY])
    {
    }

    public CustomList(int capacity) : this(new T[capacity])
    {
    }

    public CustomList(IEnumerable<T> enumerable) : this(enumerable.ToArray())
    {
        this.Count = this.elements.Length;
    }

    public int Count { get; private set; }

    public int Capacity => this.elements.Length;

    public T this[int index]
    {
        get
        {
            this.EnsureIndex(index);

            return this.elements[index];
        }
        set
        {
            this.EnsureIndex(index);

            this.elements[index] = value;
        }
    }

    public void Swap(int index1, int index2)
    {
        this.EnsureIndex(index1);
        this.EnsureIndex(index2);

        var firstElement = this.elements[index1];
        var secondElement = this.elements[index2];
        this.elements[index1] = secondElement;
        this.elements[index2] = firstElement;
    }

    private void EnsureIndex(int index)
    {
        var isOutOfRange = index < 0 || index >= this.Count;

        if (isOutOfRange)
        {
            throw new IndexOutOfRangeException("Index is out of the bounds of the CustomList.");
        }
    }

    public int CountGreaterThan(T element)
    {
        var greaterElementsCount = 0;
        for (int pos = 0; pos < this.Count; pos++)
        {
            if (this.elements[pos].CompareTo(element) > 0)
            {
                greaterElementsCount++;
            }
        }

        return greaterElementsCount;
    }

    public void Add(T element)
    {
        if (this.Count == this.Capacity)
        {
            Array.Resize(ref this.elements, this.Capacity * 2);
        }

        this.elements[this.Count] = element;
        this.Count++;
    }

    public T Remove(int index)
    {
        this.EnsureIndex(index);

        var element = this.elements[index];

        for (int pos = index; pos < this.Count; pos++)
        {
            this.elements[pos] = this.elements[pos + 1];
        }

        this.Count--;
        if (this.Count < this.Capacity / 4)
        {
            Array.Resize(ref this.elements, this.Capacity / 2);
        }

        return element;
    }

    public bool Contains(T element)
    {
        for (int pos = 0; pos < this.Count; pos++)
        {
            if (this.elements[pos].Equals(element))
            {
                return true;
            }
        }

        return false;
    }

    public T Max()
    {
        this.EnsureNotEmptyArray();

        var max = this.elements[0];
        for (int pos = 1; pos < this.Count; pos++)
        {
            if (this.elements[pos].CompareTo(max) > 0)
            {
                max = this.elements[pos];
            }
        }

        return max;
    }

    public T Min()
    {
        this.EnsureNotEmptyArray();

        var min = this.elements[0];
        for (int pos = 1; pos < this.Count; pos++)
        {
            if (this.elements[pos].CompareTo(min) < 0)
            {
                min = this.elements[pos];
            }
        }

        return min;
    }

    private void EnsureNotEmptyArray()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("CustomList contains no elements.");
        }
    }

    public void Sort(IComparer<T> comparer = null)
    {
        Array.Sort(this.elements, 0, this.Count, comparer);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int pos = 0; pos < this.Count; pos++)
        {
            yield return this.elements[pos];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}