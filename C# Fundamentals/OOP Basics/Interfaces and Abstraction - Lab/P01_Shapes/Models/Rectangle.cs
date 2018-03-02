using System;

namespace P01_Shapes.Models
{
    public class Rectangle : IDrawable
    {
        private int _width;
        private int _height;
        public int Width
        {
            get => _width;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                _width = value;
            }
        }

        public int Height
        {
            get => _height;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                _height = value;
            }
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void Draw()
        {
            DrawLine('*', '*');
            for (int i = 1; i < Height - 1; i++)
            {
                DrawLine('*', ' ');
            }
            DrawLine('*', '*');
        }

        private void DrawLine(char end, char mid)
        {
            Console.Write(end);
            for (int i = 0; i < Width - 1; i++)
            {
                Console.Write(mid);
            }

            Console.WriteLine(end);
        }
    }
}