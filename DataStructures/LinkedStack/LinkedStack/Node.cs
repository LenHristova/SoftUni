using System;

class Node<T>
{
    public T Value { get; set; }

    public Node<T> Next { get; set; }

    public Node(T value, Node<T> next = null)
    {
        Value = value;
        Next = next;
    }
}