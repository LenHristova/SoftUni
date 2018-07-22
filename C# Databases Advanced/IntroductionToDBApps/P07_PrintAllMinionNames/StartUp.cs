using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using P01_InitialSetup;

namespace P07_PrintAllMinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            var minionsNames = GetMinionsNames();

            for (int i = 0; i < minionsNames.Count / 2; i++)
            {
                Console.WriteLine(minionsNames[i]);
                Console.WriteLine(minionsNames[minionsNames.Count - 1 - i]);
            }

            if (minionsNames.Count % 2 == 1)
            {
                var middleMinionName = minionsNames.Count / 2;
                Console.WriteLine(minionsNames[middleMinionName]);
            }
        }

        private static IList<string> GetMinionsNames()
        {
            var minionsNames = new List<string>();

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Name FROM Minions";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var currentMinionName = (string)reader["Name"];
                            minionsNames.Add(currentMinionName);
                        }
                    }
                }

                connection.Close();
            }

            return minionsNames;
        }
    }
}
