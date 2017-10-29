using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QueryMess
{
    class StartUp
    {
        static void Main()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "END")
                    break;

                string[][] fieldsValuesList = input
                    .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries))
                    .ToArray();

                Dictionary<string, List<string>> fieldsAndValuesData = GetFieldsAndValuesData(fieldsValuesList);

                Print(fieldsAndValuesData);
            }
        }

        private static void Print(Dictionary<string, List<string>> fieldsAndValuesData)
        {
            foreach (var fieldValuesPair in fieldsAndValuesData)
            {
                string field = fieldValuesPair.Key;
                string values = string.Join(", ", fieldValuesPair.Value);
                Console.Write($"{field}=[{values}]");
            }
            Console.WriteLine();
        }

        private static Dictionary<string, List<string>> GetFieldsAndValuesData(string[][] fieldsValuePairsList)
        {
            Dictionary<string, List<string>> fieldsValuePairs = new Dictionary<string, List<string>>();
            foreach (var fieldValuePair in fieldsValuePairsList)
            {
                string field = GetValidStr(fieldValuePair[0]);
                string value = GetValidStr(fieldValuePair[1]);

                if (!fieldsValuePairs.ContainsKey(field))
                {
                    fieldsValuePairs[field] = new List<string>();
                }
                fieldsValuePairs[field].Add(value);
            }

            return fieldsValuePairs;
        }

        private static string GetValidStr(string str)
        {
            string res = str;
            if (str.Contains('?'))
            {
                res = Regex.Match(str, @"(?<=\?).*").Value;
            }
            if (res.Contains("%20") || res.Contains('+'))
            {
                res = res.Replace("%20", " ").Replace("+", " ").Trim();
                res = Regex.Replace(res, @"\s{2,}", @" ");
            }
            return res;
        }
    }
}
