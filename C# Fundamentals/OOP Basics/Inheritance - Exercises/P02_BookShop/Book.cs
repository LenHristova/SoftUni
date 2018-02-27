using System;
using System.Linq;

namespace P02_BookShop
{
    public class Book
    {
        private string _author;
        private string _title;
        private decimal _price;

        protected string Author
        {
            get => _author;
            set
            {
                var names = value.Split();
                if (names.Length > 1 && char.IsDigit(names[1].First()))
                {
                    throw new ArgumentException("Author not valid!");
                }

                _author = value;
            }
        }

        protected string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new ArgumentException("Title not valid!");
                }

                _title = value;
            }
        }

        protected virtual decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price not valid!");
                }

                _price = value;
            }
        }

        public Book(string author, string title, decimal price)
        {
            Author = author;
            Title = title;
            Price = price;
        }

        public override string ToString()
        {
            return $"Type: {GetType().Name}{Environment.NewLine}" +
                   $"Title: {Title}{Environment.NewLine}" +
                   $"Author: {Author}{Environment.NewLine}" +
                   $"Price: {Price:F2}";
        }
    }
}