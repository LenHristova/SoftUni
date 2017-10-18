using System;

class StartUp
{
    static void Main()
    {
        string[] words = Console.ReadLine()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        Random rnd = new Random();

        for (int pos1 = 0; pos1 < words.Length; pos1++)
        {
            int pos2 = rnd.Next(words.Length);
            string old = words[pos1];
            words[pos1] = words[pos2];
            words[pos2] = old;
        }

        Console.WriteLine(string.Join(Environment.NewLine, words));
    }    
}