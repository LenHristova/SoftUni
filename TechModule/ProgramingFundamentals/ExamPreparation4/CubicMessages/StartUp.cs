using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CubicMessages
{
    class StartUp
    {
        static void Main()
        {


            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Over!")
                    break;

                int m = int.Parse(Console.ReadLine());

                string validMasagePattern = $@"^(?<leftDigits>\d+)(?<masage>[A-Za-z]{{{m}}})(?<rightDigits>[^A-Za-z]*)$";
                bool isValidMasage = Regex.IsMatch(input, validMasagePattern);

                if (!isValidMasage)
                {
                    continue;
                }

                var encryptedMasage = Regex.Match(input, validMasagePattern)
                    .Groups;

                //taking indexes before masage
                List<int> indexes = Regex.Matches(encryptedMasage[1].Value, @"\d")
                    .Cast<Match>()
                    .Select(match => int.Parse(match.Value))
                    .ToList();

                List<int> lastIndexes = Regex.Matches(encryptedMasage[3].Value, @"\d")
                    .Cast<Match>()
                    .Select(match => int.Parse(match.Value))
                    .ToList();

                indexes.AddRange(lastIndexes);

                string masage = encryptedMasage[2].Value;

                StringBuilder sb = new StringBuilder();
                foreach (int index in indexes)
                {
                    if (index < masage.Length)
                    {
                        sb.Append(masage[index]);
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }

                Console.WriteLine($"{masage} == {sb}");
            }
        }
    }
}
