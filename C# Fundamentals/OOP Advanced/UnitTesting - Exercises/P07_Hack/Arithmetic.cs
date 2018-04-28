using System;

namespace P07_Hack
{
    public class Arithmetic : IAbsolutable, IFloorable
    {
        public float ReturnAbsValue(float value)
        {
            return Math.Abs(value);
        }

        public int ReturnAbsValue(int value)
        {
            return Math.Abs(value);
        }

        public double ReturnAbsValue(double value)
        {
            return Math.Abs(value);
        }

        public long ReturnAbsValue(long value)
        {
            return Math.Abs(value);
        }

        public decimal ReturnAbsValue(decimal value)
        {
            return Math.Abs(value);
        }

        public double ReturnFlooredValue(double value)
        {
            return Math.Floor(value);
        }

        public decimal ReturnFlooredValue(decimal value)
        {
            return Math.Floor(value);
        }
    }
}
