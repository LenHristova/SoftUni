using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayStack<T> : IEnumerable<T>
{
    private T[] _array;

    public int Count { get; set; }

    public ArrayStack(int capacity = 16)
    {
        this._array = new T[capacity];
        this.Count = 0;
    }

    public void Push(T element)
    {
        if (this.Count == this._array.Length)
        {
            Array.Resize(ref this._array, this._array.Length * 2);
        }

        this._array[this.Count++] = element;
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("ArrayStack empty!");
        }

        var element = this._array[--this.Count];
        this._array[this.Count] = default(T);

        if (this.Count < this._array.Length / 4)
        {
            Array.Resize(ref this._array, this._array.Length / 2);
        }

        return element;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("ArrayStack empty!");
        }

        return this._array[this.Count - 1];
    }

    public void Clear()
    {
        this._array = new T[16];
        this.Count = 0;
    }

    public void TrimExcesss()
    {
        var copy = new T[this.Count];
        Array.Copy(this._array, copy, this.Count);
        this._array = copy;
    }

    public T[] ToArray()
    {
        var copy = new T[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            copy[i] = this._array[this.Count - i - 1];
        }

        return copy;
    }

    public bool Contains(T element)
    {
        for (int i = 0; i < this.Count; i++)
        {
            if (this._array[i].Equals(element))
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(T[] array, int startIndex)
    {
        Array.Copy(this.ToArray(), 0, array, startIndex, this.Count);
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            yield return this._array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool IsEmpty()
    {
        return this.Count == 0;
    }
}