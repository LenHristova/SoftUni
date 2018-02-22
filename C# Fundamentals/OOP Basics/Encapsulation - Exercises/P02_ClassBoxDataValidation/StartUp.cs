using System;

namespace P02_ClassBoxDataValidation
{
    class StartUp
    {
        static void Main()
        {
            var box = new Box();
            try
            {
                box.GetParameters();
            }
            catch (FormatException fex)
            {
                Console.WriteLine(fex.Message);
                return;
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
                return;
            }

            Console.WriteLine($"Surface Area - {box.SurfaceArea():F2}");
            Console.WriteLine($"Lateral Surface Area - {box.LateralSurfaceArea():F2}");
            Console.WriteLine($"Volume - {box.Volume():F2}");
        }
    }
}