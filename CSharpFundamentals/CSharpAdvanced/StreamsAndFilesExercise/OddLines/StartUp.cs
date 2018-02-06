using System;
using System.IO;

class StartUp
{
    static void Main()
    {
        using (var stream = new StreamReader("text.txt"))
        {
            var counter = 0;
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                if (counter % 2 != 0)
                {
                    Console.WriteLine(line);
                }

                counter++;
            }
        }
    }
}