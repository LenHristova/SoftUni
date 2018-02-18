namespace P12_Google
{
    using System;

    public class Pokemon
    {
        private string _name;
        private string _type;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Pokemon(string name, string type)
        {
            _name = name;
            _type = type;
        }

        public override string ToString()
        {
            return $"{Environment.NewLine}{Name} {Type}";
        }
    }
}