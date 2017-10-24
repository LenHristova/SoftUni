using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BookLibraryModification
{
    class StartUp
    {
        static void Main()
        {
            File.Delete(@"..\..\output.txt");
            var lines = File.ReadAllLines(@"..\..\input.txt")
                .ToList();

            lines.RemoveAt(0);

            var date = DateTime.ParseExact(lines.Last(),
                "dd.MM.yyyy", CultureInfo.InvariantCulture);

            lines.RemoveAt(lines.Count - 1);

            var myLibrary = GetBooks(lines);

            var titlesReleaseDate = myLibrary.Books
                .Where(b => b.Date > date)
                .OrderBy(b => b.Date)
                .ThenBy(b => b.Title)
                .ToList();

            foreach (var book in titlesReleaseDate)
            {
                File.AppendAllText(@"..\..\output.txt",
                    $"{book.Title} -> {book.Date:dd.MM.yyyy}{Environment.NewLine}");
            }
        }

        private static Library GetBooks(List<string> lines)
        {
            var myLibrary = new Library();

            foreach (var line in lines)
            {
                var bookInfo = line
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                myLibrary.Books.Add(new Book
                {
                    Title = bookInfo[0],
                    Author = bookInfo[1],
                    Publisher = bookInfo[2],
                    Date = DateTime.ParseExact(bookInfo[3],
                        "dd.MM.yyyy", CultureInfo.InvariantCulture),
                    Isbn = bookInfo[4],
                    Price = decimal.Parse(bookInfo[5])
                });
            }

            return myLibrary;
        }
    }
}
