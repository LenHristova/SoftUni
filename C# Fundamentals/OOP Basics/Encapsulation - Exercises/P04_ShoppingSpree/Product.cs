namespace P04_ShoppingSpree
{
    public class Product
    {
        private string _name;
        private decimal _cost;

        public string Name
        {
            get => _name;
            private set
            {
                Validation.ValidateName(value);
                _name = value;
            }
        }

        public decimal Cost
        {
            get => _cost;
            private set
            {
                Validation.ValidateMoney(value);
                _cost = value;
            }
        }

        public Product(string personInfo)
        {
            var info = personInfo.Split('=');

            Name = info[0];
            Cost = decimal.Parse(info[1]);
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}