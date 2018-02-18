namespace P05_GreedyTimes
{
    public class Item
    {
        public string Name { get; set; }
        public long Amount { get; set; }
        public string Type { get; set; }

        public Item(string name, string type)
        {
            Name = name;
            Amount = 0;
            Type = type;
        }

        public override string ToString()
        {
            return $"##{Name} - {Amount}";
        }
    }
}