using System;
using System.Collections.Generic;
using System.Linq;

class StartUp
{
    static void Main()
    {
        string[] words = Console.ReadLine().ToLower()
            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        Dictionary<string, int> counts = GetDictionary(words);

        List<string> oddCountWords = new List<string>();
        foreach (var count in counts)
        {
            if (count.Value % 2 !=0)
            {
                oddCountWords.Add(count.Key);
            }           
        }

        Console.WriteLine(string.Join(", ", oddCountWords));
    }

    static Dictionary<string, int> GetDictionary(string[] words)
    {
        Dictionary<string, int> counts = new Dictionary<string, int>();

        foreach (string word in words)
        {
            if (!counts.ContainsKey(word))
            {
                counts[word] = 0;
            }
            counts[word]++;
        }

        return counts;
    }
}