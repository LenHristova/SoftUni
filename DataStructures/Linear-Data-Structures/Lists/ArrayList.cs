using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayList<T> : IEnumerable<T>
{
    private const int InitialCapacity = 2;

    private T[] _items;

    public ArrayList()
    {
        _items = new T[InitialCapacity];
    }

    public int Count { get; set; }

    public T this[int index]
    {
        get
        {
            if (index >= Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _items[index];
        }

        set
        {
            if (index >= Count || index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _items[index] = value;
        }
    }

    public void Add(T item)
    {
        if (Count == _items.Length)
        {
            Resize();
        }

        _items[Count++] = item;
    }

    public T RemoveAt(int index)
    {
        var element = this[index];
        this[index] = default(T);
        Shift(index);
        Count--;

        if (Count <= _items.Length / 4)
        {
            Shrink();
        }

        return element;
    }

    private void Resize()
    {
        var copy = new T[_items.Length * 2];
        Array.Copy(_items, copy, Count);
        _items = copy;
    }

    private void Shrink()
    {
        var copy = new T[_items.Length / 2];
        Array.Copy(_items, copy, Count);

        _items = copy;
    }

    private void Shift(int index)
    {
        while (index < Count)
        {
            _items[index] = _items[++index];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in _items)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}