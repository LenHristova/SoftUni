using System.Collections.Generic;

public class Box<T>
{
    private readonly IList<T> elements;

    public Box()
    {
        this.elements = new List<T>();
    }

    public int Count => this.elements.Count;

    public void Add(T element)
    {
        this.elements.Add(element);
    }

    public T Remove()
    {
        var lastIndex = this.Count - 1;
        var element = this.elements[lastIndex];
        this.elements.RemoveAt(lastIndex);

        return element;
    }
}