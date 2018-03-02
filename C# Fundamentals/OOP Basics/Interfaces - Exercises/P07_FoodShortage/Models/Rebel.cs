namespace P07_FoodShortage.Models
{
    public class Rebel : Buyer, IBuyer, IPerson
    {
        private const int POSSIBLE_FOOD_PURCHASE = 5;

        public string Group { get; set; }

        public Rebel(string name, int age, string group) : base(name, age, POSSIBLE_FOOD_PURCHASE)
        {
            Group = group;
        }
    }
}