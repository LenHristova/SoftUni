using System;

namespace P02_BookShop
{
    class StartUp
    {
        static void Main()
        {
            try
            {
                var author = Console.ReadLine();
                var title = Console.ReadLine();
                var price = decimal.Parse(Console.ReadLine());

                var book = new Book(author, title, price);
                var goldenEditionBook = new GoldenEditionBook(author, title, price);

                Console.WriteLine(book);
                Console.WriteLine();
                Console.WriteLine(goldenEditionBook);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("Price must be a number.");
            }
        }
    }
}
