namespace P08_RawData
{
    public class Tire
    {
        private double _pressure;
        private int _age;

        public double Pressure
        {
            get => _pressure;
            set => _pressure = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public Tire(double pressure, int age)
        {
            _pressure = pressure;
            _age = age;
        }

        public bool HasPressure()
        {
            return Pressure >= 1;
        }
    }
}