using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var stack = new Stack<int>();
        var linkedStack = new LinkedStack<int>();

        //Console.WriteLine(stack.Pop());
        //Console.WriteLine(linkedStack.Pop());
        //Console.WriteLine(stack.Peek());
        //Console.WriteLine(linkedStack.Peek());

        Console.WriteLine("Stack-Push-Some-Elements..............................................");
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
        Console.WriteLine("LinkedStack-Push-Some-Elements.........................................");
        linkedStack.Push(1);
        linkedStack.Push(2);
        linkedStack.Push(3);
        linkedStack.Push(4);
        linkedStack.Push(5);

        foreach (var arrSt in linkedStack)
        {
            Console.Write(arrSt + " ");
        }

        Console.WriteLine();

        Console.WriteLine("Stack-Push-50-Elements..............................................");
        for (int i = 0; i < 50; i++)
        {
            stack.Push(i);
        }

        Console.WriteLine(string.Join(", ", stack.ToArray()));

        Console.WriteLine("LinkedStack-Push-50-Elements.........................................");
        for (int i = 0; i < 50; i++)
        {
            linkedStack.Push(i);
        }

        Console.WriteLine(string.Join(", ", linkedStack.ToArray()));


        Console.WriteLine("Stack-Pop-50-Elements..............................................");
        for (int i = 0; i < 50; i++)
        {
            stack.Pop();
        }
        Console.WriteLine(string.Join(", ", stack.ToArray()));

        Console.WriteLine("LinkedStack-Pop-50-Elements.........................................");
        for (int i = 0; i < 50; i++)
        {
            linkedStack.Pop();
        }
        Console.WriteLine(string.Join(", ", linkedStack.ToArray()));


        Console.WriteLine("Stack-Peek-Some-Elements..............................................");
        var stackPeek = stack.Peek();
        Console.WriteLine(stackPeek);
        stackPeek = stack.Peek();
        Console.WriteLine(stackPeek);
        Console.WriteLine(string.Join(", ", stack.ToArray()));
        stack.Pop();
        stackPeek = stack.Peek();
        Console.WriteLine(stackPeek);
        Console.WriteLine(string.Join(", ", stack.ToArray()));

        Console.WriteLine("LinkedStack-Peek-Some-Elements.........................................");
        var linkedStackPeek = linkedStack.Peek();
        Console.WriteLine(linkedStackPeek);
        linkedStackPeek = linkedStack.Peek();
        Console.WriteLine(linkedStackPeek);
        Console.WriteLine(string.Join(", ", linkedStack.ToArray()));
        linkedStack.Pop();
        linkedStackPeek = stack.Peek();
        Console.WriteLine(linkedStackPeek);
        Console.WriteLine(string.Join(", ", linkedStack.ToArray()));


        Console.WriteLine("Stack..............................................");
        Console.Write("Contains 3?");
        Console.WriteLine(stack.Contains(3) ? " - Yes" : " - No");
        Console.Write("Contains 30?");
        Console.WriteLine(stack.Contains(30) ? " - Yes" : " - No");

        Console.WriteLine("LinkedStack.........................................");
        Console.Write("Contains 3?");
        Console.WriteLine(linkedStack.Contains(3) ? " - Yes" : " - No");
        Console.Write("Contains 30?");
        Console.WriteLine(linkedStack.Contains(30) ? " - Yes" : " - No");


        Console.WriteLine("Stack-CopyTo..............................................");
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

        Console.WriteLine("LinkedStack-CopyTo.........................................");
        Console.WriteLine(string.Join(", ", linkedStack.ToArray()));
        var arr4 = new int[linkedStack.Count];
        linkedStack.CopyTo(arr4, 0);
        Console.WriteLine(string.Join(", ", arr4));
        var arr5 = new int[] { 1, 2, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        linkedStack.CopyTo(arr5, 5);
        Console.WriteLine(string.Join(", ", arr5));
        var arr6 = new int[10];
        linkedStack.CopyTo(arr6, 5);
        Console.WriteLine(string.Join(", ", arr6));


        Console.WriteLine("Stack-Clear..............................................");
        stack.Clear();
        Console.WriteLine(string.Join(", ", stack.ToArray()));

        Console.WriteLine("LinkedStack-Clear.........................................");
        linkedStack.Clear();
        Console.WriteLine(string.Join(", ", linkedStack.ToArray()));
    }
}