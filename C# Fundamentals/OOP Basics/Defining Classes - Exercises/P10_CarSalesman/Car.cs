namespace P10_CarSalesman
{
    using System;

    public class Car
    {
        private string _model;
        private Engine _engine;
        private string _weight;
        private string _color;

        public string Model
        {
            get => _model;
            set => _model = value;
        }

        public Engine Engine
        {
            get => _engine;
            set => _engine = value;
        }

        public string Weight
        {
            get => _weight;
            set => _weight = value;
        }

        public string Color
        {
            get => _color;
            set => _color = value;
        }

        public Car(string model, Engine engine)
        {
            _model = model;
            _engine = engine;
            _weight = "n/a";
            _color = "n/a";
        }

        public override string ToString()
        {
            return $"{Model}:{Environment.NewLine}" +
                   $"{Engine}{Environment.NewLine}" +
                   $"  Weight: {Weight}{Environment.NewLine}" +
                   $"  Color: {Color}";
        }
    }
}