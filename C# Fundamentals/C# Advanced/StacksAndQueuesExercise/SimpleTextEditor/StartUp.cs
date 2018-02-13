using System;
using System.Collections.Generic;
using System.Text;

class StartUp
{
    private static StringBuilder _text = new StringBuilder();
    private static readonly Stack<string> OldVersions = new Stack<string>();

    static void Main()
    {
        var commandsCount = int.Parse(Console.ReadLine());

        for (var i = 0; i < commandsCount; i++)
        {
            var commandsArgs = Console.ReadLine()
                .Split(new[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            switch (commandsArgs[0])
            {
                case "1":
                    OldVersions.Push(_text.ToString());
                    var str = commandsArgs[1];
                    Add(str);
                    break;
                case "2":
                    OldVersions.Push(_text.ToString());
                    var count = int.Parse(commandsArgs[1]);
                    Erase(count);
                    break;
                case "3":
                    var index = int.Parse(commandsArgs[1]);
                    Console.WriteLine(CharAt(index));
                    break;
                case "4":
                    Undo();
                    break;
            }
        }
    }

    private static void Undo()
    {
        if (OldVersions.Count > 0)
        {
            _text = new StringBuilder(OldVersions.Pop());
        }
    }

    private static char CharAt(int index)
    {
        return _text.ToString()[index - 1];
    }

    private static void Erase(int count)
    {
        var startIndex = _text.Length - count;
        _text.Remove(startIndex, count);
    }

    private static void Add(string str)
    {
        _text.Append(str);
    }
}