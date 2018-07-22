using System;
using System.Globalization;
using System.Linq;
using BookShop.Data;

using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new BookShopContext())
            {
                //DbInitializer.ResetDatabase(context);

                ////--1
                // var command = Console.ReadLine();
                //var result = GetBooksByAgeRestriction(context, command);

                ////--2
                //var result = GetGoldenBooks(context);

                ////--3
                //var result = GetBooksByPrice(context);

                ////--4
                //var year = int.Parse(Console.ReadLine());
                //var result = GetBooksNotRealeasedIn(context, year);

                ////--5
                //var input = Console.ReadLine();
                //var result = GetBooksByCategory(context, input);

                ////--6
                //var input = Console.ReadLine();
                //var result = GetBooksReleasedBefore(context, input);

                ////--7
                //var input = Console.ReadLine();
                //var result = GetAuthorNamesEndingIn(context, input);

                ////--8
                //var input = Console.ReadLine();
                //var result = GetBookTitlesContaining(context, input);

                ////--9
                //var input = Console.ReadLine();
                //var result = GetBooksByAuthor(context, input);

                ////--10
                //var lengthCheck = int.Parse(Console.ReadLine());
                //var result = CountBooks(context, lengthCheck);

                ////--11
                //var result = CountCopiesByAuthor(context);

                ////--12
                //var result = GetTotalProfitByCategory(context);

                ////--13
                var result = GetMostRecentBooks(context);

                ////--14
                //IncreasePrices(context);

                ////--15
                //var deletedBooks = RemoveBooks(context);

                //Console.WriteLine(result);
            }
        }


        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Copies < 8000)
                .ToList();

            context.Books.RemoveRange(books);
            context.SaveChanges();

            return books.Count;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            context.Books
                .Where(b => b.ReleaseDate.HasValue && b.ReleaseDate.Value.Year < 2010)
                .ToList()
                .ForEach(b => b.Price += 5);
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {

            var categoriesRecentBooks = context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    Name = c.Name,
                    RecentBooks = c.CategoryBooks
                        .OrderByDescending(b => b.Book.ReleaseDate)
                        .Take(3)
                        .Select(b => $"{b.Book.Title} ({b.Book.ReleaseDate.Value.Year})")
                })
                .Select(c => $"--{c.Name}{Environment.NewLine}" +
                             $"{string.Join(Environment.NewLine, c.RecentBooks)}")
                .ToList();

            return string.Join(Environment.NewLine, categoriesRecentBooks);
        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var categoryProfit = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    Profit = c.CategoryBooks
                        .Sum(cb => cb.Book.Copies * cb.Book.Price)
                })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.Name)
                .Select(c => $"{c.Name} ${c.Profit:F2}")
                .ToList();

            return string.Join(Environment.NewLine, categoryProfit);
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var authorCopies = context.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    BookCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.BookCopies)
                .Select(a => $"{a.Name} - {a.BookCopies}")
                .ToList();

            return string.Join(Environment.NewLine, authorCopies);
        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            var booksCount = context.Books
                .Count(b => b.Title.Length > lengthCheck);

            return booksCount;
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {

            var bookTitles = context.Books
                .Where(b => EF.Functions.Like(b.Author.LastName, $"{input}%"))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})")
                .ToList();

            return string.Join(Environment.NewLine, bookTitles);
        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var bookTitles = context.Books
                .Where(b => EF.Functions.Like(b.Title, $"%{input}%"))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, bookTitles);
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var authors = context.Authors
                .Where(a => EF.Functions.Like(a.FirstName, "%" + input))
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => $"{a.FirstName} {a.LastName}")
                .ToList();

            return string.Join(Environment.NewLine, authors);
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out var maxDate))
            {
                var books = context.Books
                  .Where(b => b.ReleaseDate < maxDate)
                  .OrderByDescending(b => b.ReleaseDate)
                  .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:F2}")
                  .ToList();

                return string.Join(Environment.NewLine, books);
            }

            return string.Empty;
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.ToLower()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var bookTitles = context.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, bookTitles);
        }

        public static string GetBooksNotRealeasedIn(BookShopContext context, int year)
        {
            var bookTitles = context.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, bookTitles);
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var books = context.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:F2}")
                .ToList();

            return string.Join(Environment.NewLine, books);
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var booksTitles = context.Books
                .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, booksTitles);
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            {
                return string.Empty;
            }

            var booksTitles = context.Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            return string.Join(Environment.NewLine, booksTitles);

        }
    }
}
