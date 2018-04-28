using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace P01_HarvestingFields
{
    using System;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            var sb = new StringBuilder();
            var allFields = typeof(HarvestingFields).GetFields(
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public
            );

            string command;
            while ((command = Console.ReadLine()) != "HARVEST")
            {
                try
                {
                    ParseCommand(allFields, command, sb);
                }
                catch (Exception e)
                {
                    sb.AppendLine(e.Message);
                }
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }

        private static void ParseCommand(IEnumerable<FieldInfo> allFields, string command, StringBuilder sb)
        {
            IEnumerable<FieldInfo> fields;
            switch (command)
            {
                case "private":
                    fields = allFields.Where(f => f.IsPrivate);
                    break;
                case "protected":
                     fields = allFields.Where(f => f.IsFamily);
                    break;
                case "public":
                     fields = allFields.Where(f => f.IsPublic);
                    break;
                case "all":
                    fields = allFields;
                    break;
                default:
                    throw new NotSupportedException();
            }

            foreach (var field in fields)
            {
                var access = string.Empty;

                switch (field.Attributes)
                {
                    case FieldAttributes.Private:
                        access = "private";
                        break;
                    case FieldAttributes.Family:
                        access = "protected";
                        break;
                    case FieldAttributes.Public:
                        access = "public";
                        break;
                }

                sb.AppendLine($"{access} {field.FieldType.Name} {field.Name}");
            }
        }
    }
}
