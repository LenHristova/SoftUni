using System;

namespace P05_PizzaCalories
{
    class StartUp
    {
        static void Main()
        {
            try
            {
                var pizzaInfo = Console.ReadLine().Split();
                var pizza = new Pizza(pizzaInfo[1]);

                var doughInfo = Console.ReadLine().Split();        
                var dough = new Dough(double.Parse(doughInfo[3]), doughInfo[1], doughInfo[2]);
                pizza.AddDough(dough);

                string input;
                while ((input = Console.ReadLine()) != "END")
                {
                    var toppingInfo = input.Split();
                    var topping = new Topping(double.Parse(toppingInfo[2]), toppingInfo[1]);
                    pizza.TryToAddTopping(topping);
                }

                Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories:F2} Calories.");
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }
        }
    }
}