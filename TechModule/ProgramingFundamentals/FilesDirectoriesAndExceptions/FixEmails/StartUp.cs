using System;
using System.IO;

namespace FixEmails
{
    class StartUp
    {
        static void Main()
        {
            File.Delete(@"..\..\output.txt");

            var lines = File.ReadAllLines(@"..\..\input.txt");

            for (var i = 0; i < lines.Length - 1; i += 2)
            {
                var name = lines[i];
                var email = lines[i + 1];

                if (email.EndsWith("uk") || email.EndsWith("us"))
                    continue;

                File.AppendAllText(@"..\..\output.txt",
                    $"{name} -> {email}{Environment.NewLine}");
            }
        }
    }
}
