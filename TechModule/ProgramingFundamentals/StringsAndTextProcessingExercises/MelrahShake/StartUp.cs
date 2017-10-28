using System;

namespace MelrahShake
{
    class StartUp
    {
        static void Main()
        {
            string str = Console.ReadLine();
            string pattern = Console.ReadLine();

            while (pattern != string.Empty && str.Length >= pattern.Length * 2)
            {
                string temp = str;

                int firstMatchIndex = temp.IndexOf(pattern);
                if (firstMatchIndex == -1)
                {
                    break;
                }

                temp = temp.Remove(firstMatchIndex, pattern.Length);

                int lastMatchIndex = temp.LastIndexOf(pattern);

                if (lastMatchIndex == -1)
                {
                    break;
                }

                Console.WriteLine("Shaked it.");
                temp = temp.Remove(lastMatchIndex, pattern.Length);
                str = temp;
                pattern = pattern.Remove(pattern.Length / 2, 1);
            }

            Console.WriteLine("No shake.");
            Console.WriteLine(str);            
        }
    }
}
