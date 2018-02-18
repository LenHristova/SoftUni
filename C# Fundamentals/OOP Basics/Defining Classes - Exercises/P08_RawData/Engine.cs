namespace P08_RawData
{
    public class Engine
    {
        private int _speed;
        private int _power;

        public int Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public int Power
        {
            get => _power;
            set => _power = value;
        }

        public Engine(int speed, int power)
        {
            _speed = speed;
            _power = power;
        }

        public bool HasPower()
        {
            return Power > 250;
        }
    }
}