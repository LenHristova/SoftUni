namespace P02_BlackBoxInteger
{
    public class BlackBoxInteger
    {
        private static int _defaultValue = 0;

        private int _innerValue;

        private BlackBoxInteger(int innerValue)
        {
            _innerValue = innerValue;
        }

        private BlackBoxInteger()
        {
            _innerValue = _defaultValue;
        }

        private void Add(int addend)
        {
            _innerValue += addend;
        }

        private void Subtract(int subtrahend)
        {
            _innerValue -= subtrahend;
        }

        private void Multiply(int multiplier)
        {
            _innerValue *= multiplier;
        }

        private void Divide(int divider)
        {
            _innerValue /= divider;
        }

        private void LeftShift(int shifter)
        {
            _innerValue <<= shifter;
        }

        private void RightShift(int shifter)
        {
            _innerValue >>= shifter;
        }
    }
}
