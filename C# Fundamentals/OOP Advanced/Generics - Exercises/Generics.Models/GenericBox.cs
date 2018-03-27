using System;

public class GenericBox<T> : IComparable<GenericBox<T>>
    where T : IComparable<T>
{
    private readonly T element;

    public GenericBox(T element)
    {
        this.element = element;
    }

    public int CompareTo(GenericBox<T> other)
    {
        if (element.CompareTo(other.element) > 0)
        {
            return 1;
        }

        if (element.CompareTo(other.element) < 0)
        {
            return -1;
        }

        return 0;
    }

    public override string ToString()
    {
        return $"{this.element.GetType().FullName}: {this.element}";
    }   
}