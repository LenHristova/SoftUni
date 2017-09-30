using System;

namespace p05_BooleanVariableVariant2
{
    class BooleanVariableVariant2
    {
        static void Main(string[] args)
        {
            string check = Console.ReadLine();
            bool isTrue = Convert.ToBoolean(check);
            if (isTrue)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
        }
    }
}
