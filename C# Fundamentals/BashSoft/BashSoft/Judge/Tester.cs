using System;
using System.IO;
using System.Linq;
using BashSoft.IO;
using BashSoft.Static_data;

namespace BashSoft.Judge
{
    public class Tester
    {
        //Compares contents from user output and expected output
        public void CompareContent(string userOutputPath, string expectedOutputPath)
        {
            OutputWriter.WriteMessageOnNewLine("Reading files...");

            var mismatchPath = GetMismatchPath(expectedOutputPath);

            //Reads user output
            var actualOutputLines = File.ReadAllLines(userOutputPath).ToArray();
            //Reads expected output
            var expectedOutputLines = File.ReadAllLines(expectedOutputPath).ToArray();

            var mismatches = GetLinesWithPossibleMismatches(
                actualOutputLines, expectedOutputLines, out var hasMismatches);

            PrintOutput(mismatches, hasMismatches, mismatchPath);
            OutputWriter.WriteMessageOnNewLine("Files read!");
        }

        private string[] GetLinesWithPossibleMismatches(
            string[] actualOutputLines, string[] expectedOutputLines, out bool hasMismatches)
        {
            hasMismatches = false;
            //If actual output lines are more or less then expected output lines -> there are mismatches
            var minOutputLines = actualOutputLines.Length;
            if (actualOutputLines.Length != expectedOutputLines.Length)
            {
                hasMismatches = true;
                minOutputLines = Math.Min(actualOutputLines.Length, expectedOutputLines.Length);
                OutputWriter.DisplayException(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
            }

            var mismatches = new string[minOutputLines];

            OutputWriter.WriteMessageOnNewLine("Comparing files...");

            //Cheks for mismatches line by line
            for (var index = 0; index < minOutputLines; index++)
            {
                var actualLine = actualOutputLines[index];
                var expectedLine = expectedOutputLines[index];

                //if lines are difrent, saves differences
                string output;
                if (actualLine != expectedLine)
                {
                    output = $"Mismatch at line {index} -- " +
                             $"expected: \"{expectedLine}\", actual: \"{actualLine}\"{Environment.NewLine}";
                    hasMismatches = true;
                }
                //else -> saves actual line
                else
                {
                    output = actualLine + Environment.NewLine;
                }

                mismatches[index] = output;
            }

            return mismatches;
        }

        //Gets path where file with mismatches will be saved
        //The file will be saved where is the file with expected output
        private string GetMismatchPath(string expectedOutputPath)
        {
            var indexOf = expectedOutputPath.LastIndexOf('\\');
            var directoryPath = expectedOutputPath.Substring(0, indexOf);
            var finalPath = directoryPath + @"\Mismatches.txt";
            return finalPath;
        }

        private void PrintOutput(string[] mismatches, bool hasMismatches, string mismatchPath)
        {
            if (hasMismatches)
            {
                foreach (var line in mismatches)
                {
                    OutputWriter.WriteMessageOnNewLine(line);
                }

                try
                {
                    File.WriteAllLines(mismatchPath, mismatches);
                }
                catch (DirectoryNotFoundException)
                {
                    OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
                }
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine("Files are identical. There are no mismatches.");
            }
        }
    }
}