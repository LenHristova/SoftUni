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

class StartUp
{
    static void Main()
    {
        Library myLibrary = new Library();

        int booksCount = int.Parse(Console.ReadLine());

        for (int currBook = 0; currBook < booksCount; currBook++)
        {
            string[] bookInfo = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

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

        DateTime date = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy",
            CultureInfo.InvariantCulture);

        List<Book> titlesReleaseDate = myLibrary.Books
            .Where(b => b.Date > date)
            .OrderBy(b => b.Date)
            .ThenBy(b => b.Title)
            .ToList();

        foreach (var book in titlesReleaseDate)
        {
            Console.WriteLine($"{book.Title} -> {book.Date:dd.MM.yyyy}");
        }
    }
}