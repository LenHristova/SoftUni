using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public DateTime Date { get; set; }
    public string Isbn { get; set; }
    public decimal Price { get; set; }
}

class Library
{
    public string Name { get; set; }
    public List<Book> Books { get; set; } = new List<Book>();
}

class Program
{
    static void Main()
    {
        Library myLibrary = new Library();

        int booksCount = int.Parse(Console.ReadLine());

        for (int currBook = 0; currBook < booksCount; currBook++)
        {
            string[] bookInfo = Console.ReadLine()
                .Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);

            myLibrary.Books.Add(new Book
            {
                Title = bookInfo[0],
                Author = bookInfo[1],
                Publisher = bookInfo[2],
                Date = DateTime.ParseExact(bookInfo[3], "dd.MM.yyyy",
                CultureInfo.InvariantCulture),
                Isbn = bookInfo[4],
                Price = decimal.Parse(bookInfo[5])
            });
        }

        Dictionary<string, decimal> authorsPrices = myLibrary.Books
            .GroupBy(book => book.Author)
            .ToDictionary(group => group.Key, 
            group => group.Select(book => book.Price).Sum());

        authorsPrices = authorsPrices
            .OrderByDescending(kvp => kvp.Value)
            .ThenBy(kvp => kvp.Key)
            .ToDictionary(kvp => kvp.Key, k => k.Value);

        foreach (var author in authorsPrices)
        {
             Console.WriteLine(string.Join("", $"{author.Key} -> {author.Value:F2}"));
        }
    }
}