using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public class Node
    {
        public Node(T value)
        {
            Value = value;
        }

        public T Value { get; set; }

        public Node Next { get; set; }

        public Node Prev { get; set; }
    }

    private Node _head;
    private Node _tail;

    public int Count { get; private set; }

    public LinkedList()
    {
    }

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
            this._head.Prev = newNode;
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
            newNode.Prev = this._tail;
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

        var element = this._head.Value;

        if (this.Count == 1)
        {
            this._tail = this._head = null;
        }
        else
        {
            this._head = this._head.Next;
            this._head.Prev = null;
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

        var element = this._tail.Value;

        if (this.Count == 1)
        {
            this._head = this._tail = null;
        }
        else
        {
            this._tail = this._tail.Prev;
            this._tail.Next = null;
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
}