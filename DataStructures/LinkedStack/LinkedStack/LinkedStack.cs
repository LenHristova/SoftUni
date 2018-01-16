using System;
using System.Collections;
using System.Collections.Generic;

class LinkedStack<T> : IEnumerable<T>
{
    private Node<T> _first;

    public int Count { get; set; }

    public void Push(T element)
    {
        this._first = new Node<T>(element, this._first);
        this.Count++;
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("LinkedStack is empty!");
        }

        var element = this._first.Value;
        var next = this._first.Next;
        this._first = null;
        this._first = next;
        this.Count--;

        return element;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("LinkedStack is empty!");
        }

        return this._first.Value;
    }

    public void Clear()
    {

        for (int i = 0; i < this.Count; i++)
        {
            var current = this._first;
            this._first = this._first.Next;
            current = null;
        }

        this.Count = 0;
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];

        var current = this._first;
        for (int i = 0; i < this.Count; i++)
        {
            arr[i] = current.Value;
            current = current.Next;
        }

        return arr;
    }

    public bool Contains(T element)
    {
        var current = this._first;
        while (current != null)
        {
            if (current.Value.Equals(element))
            {
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    public void CopyTo(T[] array, int startIndex)
    {
        var copy = this.ToArray();
        Array.Copy(copy, 0, array, startIndex, this.Count);
    }

    private bool IsEmpty()
    {
        return this.Count == 0;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this._first;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}