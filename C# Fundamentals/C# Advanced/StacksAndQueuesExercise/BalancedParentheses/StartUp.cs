using System;
using System.Collections.Generic;

class StartUp
{
    static void Main()
    {
        var parenthesesSequence = Console.ReadLine();

        var stack = new Stack<char>();

        foreach (var parenthesis in parenthesesSequence)
        {
            if (IsOpenParenthesis(parenthesis))
            {
                stack.Push(parenthesis);
                continue;
            }

            if (IsCloseParenthesis(parenthesis))
            {
                if (stack.Count == 0)
                {
                    Console.WriteLine("NO");
                    return;
                }

                var openParenthesis = stack.Peek();
                var closeParenthesis = parenthesis;

                if (AreParenthesesCouple(openParenthesis, closeParenthesis))
                {
                    stack.Pop();
                }
                else
                {
                    Console.WriteLine("NO");
                    return;
                }
            }
        }

        Console.WriteLine(stack.Count == 0 ? "YES" : "NO");
    }

    private static bool AreParenthesesCouple(char openParenthesis, char closeParenthesis)
    {
        switch (openParenthesis)
        {
            case '{':
                if (closeParenthesis == '}')
                {
                    return true;
                }
                break;
            case '[':
                if (closeParenthesis == ']')
                {
                    return true;
                }
                break;
            case '(':
                if (closeParenthesis == ')')
                {
                    return true;
                }
                break;
        }

        return false;
    }

    private static bool IsCloseParenthesis(char parenthesis)
    {
        return parenthesis == '}' || parenthesis == ']' || parenthesis == ')';
    }

    private static bool IsOpenParenthesis(char parenthesis)
    {
        return parenthesis == '{' || parenthesis == '[' || parenthesis == '(';
    }
}