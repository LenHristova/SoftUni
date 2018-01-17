using System;
using System.Collections;
using System.Collections.Generic;

class ReversedList<T> : IEnumerable<T>
{
	private T[] _elements;

    public int Count { get; private set; }
    public int Capacity { get; private set; }

    public ReversedList(int capacity = 2)
    {
		this._elements = new T[capacity];
        this.Capacity = capacity;
        this.Count = 0;
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
            this.Capacity *= 2;
            Array.Resize(ref this._elements, this.Capacity);
        }

        this._elements[this.Count++] = element;
    }

    public void RemoveAt(int index)
    {
        if (this.Count < this.Capacity / 4)
        {
            this.Capacity /= 2;
            Array.Resize(ref this._elements, this.Capacity);
        }

        var reversedIndex = this.Count - index - 1;
        for (int i = reversedIndex; i < this.Count - 1; i++)
        {
            this._elements[i] = this._elements[i + 1];
        }

        this.Count--;
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