using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BookLibrary
{
    class StartUp
    {
        static void Main()
        {
            File.Delete(@"..\..\output.txt");
            var lines = File.ReadAllLines(@"..\..\input.txt").ToList();

            lines.RemoveAt(0);

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

            var authorsPrices = GetAuthorsPrice(myLibrary);

            foreach (var author in authorsPrices)
            {
                var res = string.Join("", $"{author.Key} -> {author.Value:F2}");
                File.AppendAllText(@"..\..\output.txt", res + Environment.NewLine);
            }
        }

        private static Dictionary<string, decimal> GetAuthorsPrice(Library myLibrary)
        {
            return myLibrary.Books
                .GroupBy(book => book.Author)
                .ToDictionary(group => group.Key, group => group.Select(book => book.Price).Sum())
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key)
                .ToDictionary(kvp => kvp.Key, k => k.Value);
        }
    }
}
