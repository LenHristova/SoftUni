namespace P12_Google
{
    using System;

    public class Car
    {
        private string _model;
        private int _speed;

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Car()
        {
        }

        public Car(string model, int speed)
        {
            _model = model;
            _speed = speed;
        }

        public override string ToString()
        {
            return Model == null ? string.Empty : $"{Environment.NewLine}{Model} {Speed}";
        }
    }
}