namespace P10_CarSalesman
{
    using System;

    public class Engine
    {
        private string _model;
        private string _power;
        private string _displacement;
        private string _efficiency;

        public string Model
        {
            get => _model;
            set => _model = value;
        }

        public string Power
        {
            get => _power;
            set => _power = value;
        }

        public string Displacement
        {
            get => _displacement;
            set => _displacement = value;
        }

        public string Efficiency
        {
            get => _efficiency;
            set => _efficiency = value;
        }

        public Engine(string model, string power)
        {
            _model = model;
            _power = power;
            _displacement = "n/a";
            _efficiency = "n/a";
        }

        public override string ToString()
        {
            return $"  {Model}:{Environment.NewLine}" +
                   $"    Power: {Power}{Environment.NewLine}" +
                   $"    Displacement: {Displacement}{Environment.NewLine}" +
                   $"    Efficiency: {Efficiency}";
        }
    }
}