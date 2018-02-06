namespace BashSoft.SimpleJudge
{
    using System;
    using System.IO;
    using System.Linq;

    public static class Tester
    {
        public static void CompareContent(string userOutputPath, string expectedOutputPath)
        {
            OutputWriter.WriteMessageOnNewLine("Reading files...");

            var mismatchPath = GetMismatchPath(expectedOutputPath);

            var actualOutputLines = File.ReadLines(userOutputPath).ToArray();
            var expectedOutputLines = File.ReadLines(expectedOutputPath).ToArray();

            var mismatches = GetLinesWithPossibleMismatches(
                actualOutputLines, expectedOutputLines, out var hasMismatches);

            PrintOutput(mismatches, hasMismatches, mismatchPath);
            OutputWriter.WriteMessageOnNewLine("Files read!");
        }

        private static void PrintOutput(string[] mismatches, bool hasMismatches, string mismatchPath)
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

        private static string[] GetLinesWithPossibleMismatches(
            string[] actualOutputLines, string[] expectedOutputLines, out bool hasMismatches)
        {
            hasMismatches = false;
            var minOutputLines = actualOutputLines.Length;
            if (actualOutputLines.Length != expectedOutputLines.Length)
            {
                hasMismatches = true;
                minOutputLines = Math.Min(actualOutputLines.Length, expectedOutputLines.Length);
                OutputWriter.DisplayException(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
            }

            var output = string.Empty;
            var mismatches = new string[minOutputLines];

            OutputWriter.WriteMessageOnNewLine("Comparing files...");
            for (int index = 0; index < minOutputLines; index++)
            {
                var actualLine = actualOutputLines[index];
                var expectedLine = expectedOutputLines[index];

                if (actualLine != expectedLine)
                {
                    output = $"Mismatch at line {index} -- expected: \"{expectedLine}\", actual: \"{actualLine}\"" +
                                         $"{Environment.NewLine}";
                    hasMismatches = true;
                }
                else
                {
                    output = actualLine + Environment.NewLine;
                }

                mismatches[index] = output;
            }

            return mismatches;
        }

        private static string GetMismatchPath(string expectedOutputPath)
        {
            var indexOf = expectedOutputPath.LastIndexOf('\\');
            var directoryPath = expectedOutputPath.Substring(0, indexOf);
            var finalPath = directoryPath + @"Mismatches.txt";
            return finalPath;
        }
    }
}