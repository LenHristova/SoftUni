namespace P14_CatLady
{
    public class Siamese : Cat
    {
        public int EarSize { get; set; }

        public Siamese(string name, int earSize) : base(name)
        {
            EarSize = earSize;
        }

        public override string ToString()
        {
            return $"Siamese {Name} {EarSize}";
        }
    }
}