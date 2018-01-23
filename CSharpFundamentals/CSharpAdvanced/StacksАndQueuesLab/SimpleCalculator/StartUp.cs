using System;
using System.Collections.Generic;

namespace SimpleCalculator
{
    internal class StartUp
    {
        private static void Main()
        {
            var expression = Console.ReadLine()
                ?.Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);

            var stack = new Stack<string>();

            if (expression != null)
                for (var i = expression.Length - 1; i >= 0; i--)
                {
                    stack.Push(expression[i]);
                }

            while (stack.Count > 1)
            {
                var leftOperand = int.Parse(stack.Pop());
                var operation = stack.Pop();
                var rightOperand = int.Parse(stack.Pop());

                switch (operation)
                {
                    case "+":
                        stack.Push((leftOperand + rightOperand).ToString());
                        break;
                    case "-":
                        stack.Push((leftOperand - rightOperand).ToString());
                        break;
                }
            }

            Console.WriteLine(stack.Pop());
        }
    }
}