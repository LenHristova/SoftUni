using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    class Node
    {
        public T Value { get; set; }

        public Node Next { get; set; }

        public Node Prev { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node _first;
    private Node _last;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        var newNode = new Node(element);

        if (IsEmpty())
        {
            this._first = newNode;
            this._last = newNode;
        }
        else
        {
            newNode.Next = this._first;
            this._first.Prev = newNode;
            this._first = newNode;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        var newNode = new Node(element);

        if (IsEmpty())
        {
            this._first = newNode;
            this._last = newNode;
        }
        else
        {
            newNode.Prev = this._last;
            this._last.Next = newNode;
            this._last = newNode;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No elements!");
        }

        var element = this._first.Value;

        if (this.Count == 1)
        {
            this._first = null;
            this._last = null;
        }
        else
        {
            this._first = this._first.Next;
            this._first.Prev = null;
        }

        this.Count--;

        return element;
    }

    public T RemoveLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No elements!");
        }

        var element = this._last.Value;

        if (this.Count == 1)
        {
            this._first = null;
            this._last = null;
        }
        else
        {
            this._last = this._last.Prev;
            this._last.Next = null;
        }

        this.Count--;

        return element;
    }

    public void ForEach(Action<T> action)
    {
        var current = this._first;
        while (current != null)
        {
            action.Invoke(current.Value);
            current = current.Next;
        }
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

    private bool IsEmpty()
    {
        return this.Count == 0;
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
