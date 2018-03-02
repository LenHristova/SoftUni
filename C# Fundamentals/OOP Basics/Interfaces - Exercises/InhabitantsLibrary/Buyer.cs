public class Buyer : Person, IBuyer
{
    protected int PossibleFoodPurchase { get; set; }
    public int Food { get; private set; }

    public Buyer(string name, int age, int possibleFoodPurchase) : base(name, age)
    {
        PossibleFoodPurchase = possibleFoodPurchase;
        Food = 0;
    }

    public void BuyFood()
    {
        Food += PossibleFoodPurchase;
    }
}