using System;
using System.Collections.Generic;

public class BookComparator : IComparer<Book>
{
    public int Compare(Book x, Book y)
    {
        var result = x.Title.CompareTo(y.Title);

        return result == 0
            ? y.Year.CompareTo(x.Year)
            : result;
    }
}