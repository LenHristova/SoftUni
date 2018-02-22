using System;

namespace P02_ClassBoxDataValidation
{
    public class Box
    {
        private double _length;
        private double _width;
        private double _height;

        private double Length
        {
            get => _length;
            set
            {
                ValidateData(value, "Length");
                _length = value;
            }
        }

        private double Width
        {
            get => _width;

            set
            {
                ValidateData(value, "Width");
                _width = value;
            }
        }

        private double Height
        {
            get => _height;
            set
            {
                ValidateData(value, "Height");
                _height = value;
            }
        }

        private static void ValidateData(double value, string property)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"{property} cannot be zero or negative.");
            }
        }

        public void GetParameters()
        {
            try
            {
                Length = double.Parse(Console.ReadLine());
                Width = double.Parse(Console.ReadLine());
                Height = double.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                throw new FormatException("Parameters must be numbers!");
            }
        }

        public double SurfaceArea()
        {
            return 2 * (Length * Width + Length * Height + Width * Height);
        }

        public double LateralSurfaceArea()
        {
            return 2 * Height * (Length + Width);
        }
        public double Volume()
        {
            return Length * Width * Height;
        }
    }
}