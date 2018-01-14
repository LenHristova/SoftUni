using System;

public class CircularQueue<T>
{
    private T[] _array;
    private int _head;
    private int _tail;

    public int Count { get; private set; }

    public CircularQueue(int capacity = 4)
    {
        this._array = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count == this._array.Length)
        {
            Resize();
        }

        this._array[this._tail] = element;
        this._tail = (this._tail + 1) % this._array.Length;
        this.Count++;
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("The queue is empty!");
        }

        var element = this._array[this._head];
        this._head = (this._head + 1) % this._array.Length;
        this.Count--;

        return element;
    }

    public T Peek()
    {
        return this._array[this._head];
    }

    private void Resize()
    {
        var copy = new T[this.Count * 2];

        CopyAllElements(copy);

        this._array = copy;
        this._head = 0;
        this._tail = this.Count;
    }

    private void CopyAllElements(T[] newArray)
    {
        for (int i = 0; i < this.Count; i++)
        {
            var index = (this._head + i) % this.Count;
            newArray[i] = this._array[index];
        }
    }

    public T[] ToArray()
    {
        var copy = new T[this.Count];
        CopyAllElements(copy);
        return copy;
    }

    private bool IsEmpty()
    {
        return this.Count == 0;
    }
}


public class Example
{
    public static void Main()
    {
        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
