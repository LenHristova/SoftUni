using System;

namespace P01_Shapes.Models
{
    public class Circle : IDrawable
    {
        private int _radius;

        public int Radius
        {
            get => _radius;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                _radius = value;
            }
        }

        public Circle(int radius)
        {
            Radius = radius;
        }

        public void Draw()
        {
            var rIn = Radius - 0.4;
            var rOut = Radius + 0.4;

            for (double y = Radius; y >= -Radius; y--)
            {
                for (double x = -Radius; x < rOut; x += 0.5)
                {
                    var value = x * x + y * y;

                    if (value >= rIn * rIn && value <= rOut * rOut)
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }

                Console.WriteLine();
            }
        }
    }
}