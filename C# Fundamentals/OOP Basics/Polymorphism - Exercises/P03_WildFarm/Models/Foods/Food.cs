public class Food
{
    public string Type { get; }
    public int Quantity { get; }

    public Food(string type, int quantity)
    {
        Type = type;
        Quantity = quantity;
    }
}