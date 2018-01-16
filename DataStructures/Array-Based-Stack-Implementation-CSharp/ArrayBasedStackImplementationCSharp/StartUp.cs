using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayStack<T> : IEnumerable<T>
{
    public T[] _array;

    public int Count { get; set; }

    public ArrayStack(int capacity = 2)
    {
        this._array = new T[capacity];
        this.Count = 0;
    }

    public void Push(T element)
    {
        if (this.Count == this._array.Length)
        {
            ResizeUp();
        }

        this._array[this.Count] = element;
        this.Count++;
    }

    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("ArrayStack empty!");
        }

        this.Count--;
        var element = this._array[this.Count];
        this._array[this.Count] = default(T);

        if (this.Count < this._array.Length / 4)
        {
            ResizeDown();
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
        this._array = new T[2];
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
        foreach (var el in this._array)
        {
            if (el.Equals(element))
            {
                return true;
            }
        }

        return false;
    }

    public void CopyTo(T[] array, int startIndex)
    {
        var copy = new T[this.Count];
        for (int i = 0; i < this.Count; i++)
        {
            copy[i] = this._array[this.Count - i - 1];
        }
        Array.Copy(copy, 0, array, startIndex, this.Count);
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

    private void ResizeUp()
    {
        var copy = new T[this._array.Length * 2];

        Array.Copy(this._array, copy, this.Count);
        this._array = copy;
    }

    private void ResizeDown()
    {
        var copy = new T[this._array.Length / 2];

        Array.Copy(this._array, copy, this.Count);
        this._array = copy;
    }
}

class StartUp
{
    static void Main()
    {
        var stack = new Stack<int>();
        var arrayStack = new ArrayStack<int>();

        //Console.WriteLine(stack.Pop());
        //Console.WriteLine(arrayStack.Pop());

        Console.WriteLine("Stack..............................................");
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.Push(5);

        foreach (var st in stack)
        {
            Console.Write(st + " ");
        }

        Console.WriteLine();
        Console.WriteLine("ArrayStack.........................................");
        arrayStack.Push(1);
        arrayStack.Push(2);
        arrayStack.Push(3);
        arrayStack.Push(4);
        arrayStack.Push(5);

        foreach (var arrSt in arrayStack)
        {
            Console.Write(arrSt + " ");
        }

        Console.WriteLine();

        Console.WriteLine("Stack..............................................");
        for (int i = 0; i < 50; i++)
        {
            stack.Push(i);
        }

        Console.WriteLine(string.Join(", ", stack.ToArray()));

        Console.WriteLine("ArrayStack.........................................");
        for (int i = 0; i < 50; i++)
        {
            arrayStack.Push(i);
        }

        Console.WriteLine(string.Join(", ", arrayStack.ToArray()));


        Console.WriteLine("Stack..............................................");
        stack.TrimExcess();

        Console.WriteLine("ArrayStack.........................................");
        arrayStack.TrimExcesss();


        Console.WriteLine("Stack..............................................");
        for (int i = 0; i < 50; i++)
        {
            stack.Pop();
        }
        Console.WriteLine(string.Join(", ", stack.ToArray()));

        Console.WriteLine("ArrayStack.........................................");
        for (int i = 0; i < 50; i++)
        {
            arrayStack.Pop();
        }
        Console.WriteLine(string.Join(", ", arrayStack.ToArray()));


        Console.WriteLine("Stack..............................................");
        var stackPeek = stack.Peek();
        Console.WriteLine(stackPeek);
        stackPeek = stack.Peek();
        Console.WriteLine(stackPeek);
        Console.WriteLine(string.Join(", ", stack.ToArray()));
        stack.Pop();
        stackPeek = stack.Peek();
        Console.WriteLine(stackPeek);
        Console.WriteLine(string.Join(", ", stack.ToArray()));

        Console.WriteLine("ArrayStack.........................................");
        var arrayStackPeek = arrayStack.Peek();
        Console.WriteLine(arrayStackPeek);
        arrayStackPeek = arrayStack.Peek();
        Console.WriteLine(arrayStackPeek);
        Console.WriteLine(string.Join(", ", arrayStack.ToArray()));
        arrayStack.Pop();
        arrayStackPeek = stack.Peek();
        Console.WriteLine(arrayStackPeek);
        Console.WriteLine(string.Join(", ", arrayStack.ToArray()));


        Console.WriteLine("Stack..............................................");
        Console.Write("Contains 3?");
        Console.WriteLine(stack.Contains(3) ? " - Yes" : " - No");
        Console.Write("Contains 30?");
        Console.WriteLine(stack.Contains(30) ? " - Yes" : " - No");

        Console.WriteLine("ArrayStack.........................................");
        Console.Write("Contains 3?");
        Console.WriteLine(arrayStack.Contains(3) ? " - Yes" : " - No");
        Console.Write("Contains 30?");
        Console.WriteLine(arrayStack.Contains(30) ? " - Yes" : " - No");


        Console.WriteLine("Stack..............................................");
        Console.WriteLine(string.Join(", ", stack.ToArray()));
        var arr1 = new int[stack.Count];
        stack.CopyTo(arr1, 0);
        Console.WriteLine(string.Join(", ", arr1));
        var arr2 = new int[] { 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        stack.CopyTo(arr2, 5);
        Console.WriteLine(string.Join(", ", arr2));
        var arr3 = new int[10];
        stack.CopyTo(arr3, 5);
        Console.WriteLine(string.Join(", ", arr3));

        Console.WriteLine("ArrayStack.........................................");
        Console.WriteLine(string.Join(", ", arrayStack.ToArray()));
        var arr4 = new int[arrayStack.Count];
        arrayStack.CopyTo(arr4, 0);
        Console.WriteLine(string.Join(", ", arr4));
        var arr5 = new int[] { 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        arrayStack.CopyTo(arr5, 5);
        Console.WriteLine(string.Join(", ", arr5));
        var arr6 = new int[10];
        arrayStack.CopyTo(arr6, 5);
        Console.WriteLine(string.Join(", ", arr6));


        Console.WriteLine("Stack..............................................");
        stack.Clear();
        Console.WriteLine(string.Join(", ", stack.ToArray()));

        Console.WriteLine("ArrayStack.........................................");
        arrayStack.Clear();
        Console.WriteLine(string.Join(", ", arrayStack.ToArray()));
    }
}