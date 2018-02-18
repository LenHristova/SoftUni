namespace P14_CatLady
{
    public class Cymric : Cat
    {
        public double FurLength { get; set; }

        public Cymric(string name, double furLength) : base(name)
        {
            FurLength = furLength;
        }

        public override string ToString()
        {
            return $"Cymric {Name} {FurLength:F2}";
        }
    }
}