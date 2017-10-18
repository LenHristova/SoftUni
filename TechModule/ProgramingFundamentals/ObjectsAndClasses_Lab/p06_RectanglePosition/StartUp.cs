using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    public class Rectangle
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle GetPosition()
        {
            double[] recProp = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse)
                .ToArray();

            return new Rectangle
            {
                Left = recProp[0],
                Top = recProp[1],
                Width = recProp[2],
                Height = recProp[3]
            };
        }
    }
    static void Main()
    {
        Rectangle rectangle1 = new Rectangle().GetPosition();
        Rectangle rectangle2 = new Rectangle().GetPosition();

        bool isInside = IsInside(rectangle1, rectangle2);
        Console.WriteLine(isInside ? "Inside" : "Not inside");
    }

    //Whether rectangle1 is inside rectangle2
    private static bool IsInside(Rectangle rectangle1, Rectangle rectangle2)
    {
        if (rectangle1.Left >= rectangle2.Left &&
        rectangle1.Top <= rectangle2.Top &&
        rectangle1.Width + rectangle1.Left <= rectangle2.Width + rectangle2.Left &&
        rectangle1.Height + rectangle1.Top <= rectangle2.Height + rectangle2.Top)
        {
            return true;
        }
        return false;
    }
}