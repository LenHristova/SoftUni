using System;
using System.Collections.Generic;

namespace P04_ShoppingSpree
{
    public class Person
    {
        private string _name;
        private decimal _money;

        public string Name
        {
            get => _name;
            private set
            {
                Validation.ValidateName(value);
                _name = value;
            }
        }

        public decimal Money
        {
            get => _money;
            private set
            {
                Validation.ValidateMoney(value);
                _money = value;
            }
        }

        public List<Product> Products { get; }

        public Person(string personInfo)
        {
            var info = personInfo.Split('=');
            Name = info[0];
            Money = decimal.Parse(info[1]);
            Products = new List<Product>();
        }

        public void BuyProduct(Product product)
        {
            if (product.Cost > Money)
            {
                Console.WriteLine($"{Name} can't afford {product.Name}");
                return;
            }

            Console.WriteLine($"{Name} bought {product.Name}");
            Products.Add(product);
            Money -= product.Cost;
        }

        public override string ToString()
        {
            var boughtProducts = Products.Count == 0 ? "Nothing bought" : string.Join(", ", Products);
            return $"{Name} - {boughtProducts}";
        }
    }
}