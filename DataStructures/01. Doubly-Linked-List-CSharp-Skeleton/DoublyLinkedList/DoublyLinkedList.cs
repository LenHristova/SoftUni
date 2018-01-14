using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    class Node
    {
        public T Value { get; private set; }

        public Node Next { get; set; }

        public Node Prev { get; set; }

        public Node(T value)
        {
            Value = value;
        }
    }

    private Node _head;
    private Node _tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        var newNode = new Node(element);

        if (IsEmpty())
        {
            this._head = this._tail = newNode;
        }
        else
        {
            newNode.Next = this._head;
            this._head.Prev = newNode;
            this._head = newNode;
        }

        this.Count++;
    }

    public void AddLast(T element)
    {
        var newNode = new Node(element);

        if (IsEmpty())
        {
            this._head = this._tail = newNode;
        }
        else
        {
            newNode.Prev = this._tail;
            this._tail.Next = newNode;
            this._tail = newNode;
        }

        this.Count++;
    }

    public T RemoveFirst()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No elements!");
        }

        var firstElement = this._head.Value;

        if (this.Count == 1)
        {
            this._head = this._tail = null;
        }
        else
        {
            this._head = this._head.Next;
            this._head.Prev = null;
        }

        this.Count--;

        return firstElement;
    }

    public T RemoveLast()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("No elements!");
        }

        var lastElement = this._tail.Value;

        if (this.Count == 1)
        {
            this._head = this._tail = null;
        }
        else
        {
            this._tail = this._tail.Prev;
            this._tail.Next = null;
        }

        this.Count--;

        return lastElement;
    }

    public void ForEach(Action<T> action)
    {
        var current = this._head;
        while (current != null)
        {
            action(current.Value);
            current = current.Next;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this._head;
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

        var current = this._head;
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
