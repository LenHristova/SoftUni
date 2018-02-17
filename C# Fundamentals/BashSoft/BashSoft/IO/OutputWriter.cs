namespace BashSoft
{
    using System;
    using System.Collections.Generic;

    public static class OutputWriter
    {
        //Writes message to the currently set output
        public static void WriteMessage(string message)
        {
            Console.Write(message);
        }

        //Writes message and goes on a new line
        public static void WriteMessageOnNewLine(string message)
        {
            Console.WriteLine(message);
        }

        //Writes empty line
        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }

        //Changes currently set output text color to red, 
        //writes a message and then reverses the color
        public static void DisplayException(string message)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = currentColor;
        }

        ////Prints student userName and grades
        public static void PrintStudent(KeyValuePair<string, List<int>> student)
        {
            WriteMessageOnNewLine($"{student.Key} - {string.Join(", ", student.Value)}");
        }
    }
}