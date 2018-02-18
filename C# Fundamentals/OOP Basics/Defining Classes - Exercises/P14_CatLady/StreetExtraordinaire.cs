namespace P14_CatLady
{
    public class StreetExtraordinaire : Cat
    {
        public int DecibelsOfMeows { get; set; }

        public StreetExtraordinaire(string name, int decibelsOfMeows) : base(name)
        {
            DecibelsOfMeows = decibelsOfMeows;
        }

        public override string ToString()
        {
            return $"StreetExtraordinaire {Name} {DecibelsOfMeows}";
        }
    }
}