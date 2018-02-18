namespace P08_RawData
{
    using System.Linq;

    public class Car
    {
        private string _model;
        private Engine _engine;
        private Cargo _cargo;
        private Tire[] _tires;

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

        public Cargo Cargo
        {
            get => _cargo;
            set => _cargo = value;
        }

        public Tire[] Tires
        {
            get => _tires;
            set => _tires = value;
        }

        public Car(string model, Engine engine, Cargo cargo, Tire[] tires)
        {
            _model = model;
            _engine = engine;
            _cargo = cargo;
            _tires = tires;
        }

        public override string ToString()
        {
            return Model;
        }

        public bool HasTiresPressure()
        {
            return Tires.All(tire => tire.HasPressure());
        }


        public bool HasEnginePower()
        {
            return Engine.HasPower();
        }
    }
}