using System;

namespace P01_ClassBox
{
    public class Box
    {
        private double _length;
        private double _width;
        private double _height;

        public void GetParameters()
        {
            try
            {
                _length = double.Parse(Console.ReadLine());
                _width = double.Parse(Console.ReadLine());
                _height = double.Parse(Console.ReadLine());
            }

            catch (FormatException)
            {
                throw new FormatException("Parameters must be numbers!");
            }
        }

        public double SurfaceArea()
        {
            return 2 * (_length * _width + _length * _height + _width * _height);
        }

        public double LateralSurfaceArea()
        {
            return 2 * _height * (_length + _width);
        }
        public double Volume()
        {
            return _length * _width * _height;
        }
    }
}