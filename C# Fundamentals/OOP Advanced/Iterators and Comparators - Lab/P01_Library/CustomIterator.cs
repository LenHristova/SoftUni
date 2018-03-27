using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CustomIterator<T> : IEnumerator<T>
{
    private readonly T[] collection;
    private int currentIndex;

    public CustomIterator(IEnumerable<T> collection)
    {
        this.collection = collection.ToArray();
        this.currentIndex = -1;
    }

    public T Current => this.collection[currentIndex];

    object IEnumerator.Current => this.Current;

    public bool MoveNext() => ++this.currentIndex < this.collection?.Length;

    public void Reset() => throw new NotSupportedException();

    public void Dispose() { }
}