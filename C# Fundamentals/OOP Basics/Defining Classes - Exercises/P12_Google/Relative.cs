namespace P12_Google
{
    using System;

    public class Relative
    {
        private string _name;
        private string _birthday;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        public Relative(string name, string birthday)
        {
            _name = name;
            _birthday = birthday;
        }

        public override string ToString()
        {
            return $"{Environment.NewLine}{Name} {Birthday}";
        }
    }
}