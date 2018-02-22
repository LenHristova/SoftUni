using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_ShoppingSpree
{
    class StartUp
    {
        private static Dictionary<string, Person> _persons;
        private static Dictionary<string, Product> _products;

        static void Main()
        {
            try
            {
                GetPersonsInfo();
                GetProductsInfo();

                string input;
                while ((input = Console.ReadLine()) != "END")
                {
                    Shopping(input);
                }

                PrintAllOrders();
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
        }

        private static void PrintAllOrders()
        {
            if (_persons != null)
                foreach (var person in _persons.Values)
                {
                    Console.WriteLine(person);
                }
        }

        private static void Shopping(string input)
        {
            var order = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var personName = order[0];
            var productName = order[1];

            _persons?[personName].BuyProduct(_products?[productName]);
        }

        private static void GetProductsInfo()
        {
            _products = Console.ReadLine()?
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(productInfo => new Product(productInfo))
                .ToDictionary(x => x.Name, x => x);
        }

        private static void GetPersonsInfo()
        {
            _persons = Console.ReadLine()?
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(personInfo => new Person(personInfo))
                .ToDictionary(x => x.Name, x => x);
        }
    }
}