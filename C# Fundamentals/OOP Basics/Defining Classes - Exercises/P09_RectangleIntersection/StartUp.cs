namespace P09_RectangleIntersection
{
    using System;
    using System.Collections.Generic;

    class StartUp
    {
        static void Main()
        {
            var input = Console.ReadLine()?.Split();
            if (input == null || input.Length < 2) return;

            var rectangles = new Dictionary<string, Rectangle>();
            GetRectangles(input, rectangles);

            var intersectionChecksCount = int.Parse(input[1]);
            for (int check = 0; check < intersectionChecksCount; check++)
            {
                var rectanglesForCheck = Console.ReadLine()?.Split();
                if (rectanglesForCheck == null || rectanglesForCheck.Length < 2) continue;

                var firstRectangle = rectangles[rectanglesForCheck[0]];
                var secondRectangle = rectangles[rectanglesForCheck[1]];

                Console.WriteLine(firstRectangle.Intersect(secondRectangle).ToString().ToLower());
            }
        }

        private static void GetRectangles(string[] input, Dictionary<string, Rectangle> rectangles)
        {
            var rectanglesCount = int.Parse(input[0]);
            for (int rectangle = 0; rectangle < rectanglesCount; rectangle++)
            {
                var rectangleArgs = Console.ReadLine()?.Split();
                if (rectangleArgs == null || rectangleArgs.Length < 5) continue;

                var id = rectangleArgs[0];
                var width = double.Parse(rectangleArgs[1]);
                var height = double.Parse(rectangleArgs[2]);
                var x = double.Parse(rectangleArgs[3]);
                var y = double.Parse(rectangleArgs[4]);
                var topLeftCorner = new Point(x, y);

                if (!rectangles.ContainsKey(id))
                {
                    rectangles.Add(id, new Rectangle(id, width, height, topLeftCorner));
                }
            }
        }
    }
}