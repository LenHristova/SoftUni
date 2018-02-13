using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class StartUp
{
    static void Main()
    {
        var words = new HashSet<string>();

        using (var streamReader = new StreamReader("words.txt"))
        {
            string word;
            while ((word = streamReader.ReadLine()) != null)
            {
                words.Add(word.ToLower());
            }
        }

        var wordsCount = new Dictionary<string, int>();
        using (var streamReader = new StreamReader("text.txt"))
        {
            string line;
            while ((line = streamReader.ReadLine()) != null)

                foreach (var word in words)
                {
                    var wordCountInLine = Regex.Matches(line.ToLower(), $@"\b{word.ToLower()}\b").Count;

                    if (!wordsCount.ContainsKey(word))
                    {
                        wordsCount[word] = 0;
                    }

                    wordsCount[word] += wordCountInLine;
                }
        }

        using (var streamWriter = new StreamWriter("result.txt"))
        {
            foreach (var wordCount in wordsCount.OrderByDescending(w => w.Value))
            {
                streamWriter.WriteLine($"{wordCount.Key} - {wordCount.Value}");
            }
        }
    }
}