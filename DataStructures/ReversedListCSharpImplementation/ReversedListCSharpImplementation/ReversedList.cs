using System;
using System.Collections;
using System.Collections.Generic;

class ReversedList<T> : IEnumerable<T>
{
    public int Count { get; set; }
    public int Capacity { get; set; }

    private T[] _elements;

    public ReversedList(int capacity = 2)
    {
        this.Capacity = capacity;
        this._elements = new T[capacity];
    }

    public T this[int index]
    {
        get
        {
            // This is invoked when accessing Layout with the [ ].
            if (index >= 0 && index < this.Count)
            {
                // Bounds were in range, so return the stored value.
                return this._elements[this.Count - index - 1];
            }

            // Return an error string.
            throw new IndexOutOfRangeException();
        }
        set
        {
            // This is invoked when assigning to Layout with the [ ].
            if (index >= 0 && index < this.Count)
            {
                // Assign to this element slot in the internal array.
                this._elements[this.Count - index - 1] = value;
            }
        }
    }

    public void Add(T element)
    {
        if (this.Count == this.Capacity)
        {
            ResizeUp();
        }

        this._elements[this.Count] = element;
        Count++;
    }

    public void RemoveAt(int index)
    {
        if (this.Count < this.Capacity / 4)
        {
            ResizeDown();
        }

        var reversedIndex = this.Count - index - 1;
        for (int i = reversedIndex; i < this.Count - 1; i++)
        {
            this._elements[i] = this._elements[i + 1];
        }

        this.Count--;
    }

    private void ResizeDown()
    {
        this.Capacity /= 2;
        var copy = new T[this.Capacity];
        Array.Copy(this._elements, copy, this.Count);
        this._elements = copy;
    }

    private void ResizeUp()
    {
        this.Capacity *= 2;
        var copy = new T[this.Capacity];
        Array.Copy(this._elements, copy, this.Count);
        this._elements = copy;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            yield return this._elements[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}