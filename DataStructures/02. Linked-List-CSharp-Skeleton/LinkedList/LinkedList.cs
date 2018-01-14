using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public T Value { get; set; }

        public Node Next { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node _head;
    private Node _tail;

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        var newNode = new Node(item);

        if (IsEmpty())
        {
            this._head = newNode;
            this._tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            this._head = newNode;
        }

        this.Count++;
    }

    public void AddLast(T item)
    {
        var newNode = new Node(item);

        if (IsEmpty())
        {
            this._head = newNode;
            this._tail = newNode;
        }
        else
        {
            this._tail.Next = newNode;
            this._tail = newNode;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }

        var element = _head.Value;

        if (this.Count == 1)
        {
            this._tail = null;
            this._head = null;
        }
        else
        {
            this._head = _head.Next;
        }

        this.Count--;
        return element;
    }

    public T RemoveLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }

        var element = _tail.Value;

        if (this.Count == 1)
        {
            this._head = null;
            this._tail = null;
        }
        else
        {
            var secondToLast = GetSecondToLast();
            this._tail = null;
            this._tail = secondToLast;
        }

        Count--;
        return element;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var start = this._head;

        while (start != null)
        {
            yield return start.Value;
            start = start.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private bool IsEmpty()
    {
        return Count == 0;
    }

    private Node GetSecondToLast()
    {
        var secondToLast = this._head;
        while (secondToLast.Next != this._tail)
        {
            secondToLast = secondToLast.Next;
        }

        return secondToLast;
    }
}