using System;
using System.Collections.Generic;

public class Book : IComparable<Book>
{
    public Book(string title, int year, params string[] authors)
    {
        this.Title = title;
        this.Year = year;
        this.Authors = authors;
    }

    public string Title { get; private set; }

    public int Year { get; private set; }

    public IEnumerable<string> Authors { get; private set; }

    public int CompareTo(Book other)
    {
        var result = this.Year.CompareTo(other.Year);

        return result == 0 
            ? string.Compare(this.Title, other.Title, StringComparison.Ordinal) 
            :  result;
    }

    public override string ToString()
    {
        return $"{this.Title} - {this.Year}";
    }
}