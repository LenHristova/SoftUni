using System;

class Program
{
    static void Main(string[] args)
    {
        char[] alphabet = GetAlphabet();
        char[] word = Console.ReadLine().ToCharArray();
        PrintLetterIndex(word, alphabet);
    }

    static void PrintLetterIndex(char[] word, char[] alphabet)
    {
        foreach (var letter in word)
        {
            int index = Array.IndexOf(alphabet, letter);
            Console.WriteLine($"{letter} -> {index}");
        }
    }

    static char[] GetAlphabet()
    {
        char[] alphabet = new char[26];
        for (char ch = 'a'; ch <= 'z'; ch++)
        {
            alphabet[ch - 97] = ch;
        }
        return alphabet;
    }
}