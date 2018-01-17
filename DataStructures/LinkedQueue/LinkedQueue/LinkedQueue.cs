using System;

class LinkedQueue<T>
{
    private Node<T> _head;
    private Node<T> _tail;

    public int Count { get; set; }

    public void Enqueue(T element)
    {
        var newNode = new Node<T>(element);

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

    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("LinkedQueue is empty.");
        }

        var element = this._head.Value;

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
        return element;
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("LinkedQueue is empty!");
        }

        return this._head.Value;
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