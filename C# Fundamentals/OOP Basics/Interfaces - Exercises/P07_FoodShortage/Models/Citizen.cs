using System;

namespace P07_FoodShortage.Models
{
    public class Citizen : Buyer, IIdentifiable, IBirthable, IBuyer, IPerson
    {
        private const int POSSIBLE_FOOD_PURCHASE = 10;

        public string Id { get; set; }
        public DateTime Birthdate { get; set; }

        public Citizen(string name, int age, string id, DateTime birthdate) : base(name, age, POSSIBLE_FOOD_PURCHASE)
        {
            Id = id;
            Birthdate = birthdate;
        }
    }
}