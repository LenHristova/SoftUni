namespace P08_RawData
{
    public class Cargo
    {
        private int _weight;
        private string _type;

        public int Weight
        {
            get => _weight;
            set => _weight = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public Cargo(int weight, string type)
        {
            _weight = weight;
            _type = type;
        }
    }
}