using System;
using System.Data.SqlClient;
using System.Linq;
using P01_InitialSetup;

namespace P08_IncreaseMinionAge
{
    public class StartUp
    {
        public static void Main()
        {
            var minionsIds = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            UpdateMinions(minionsIds);
            PrintAllMinions();
        }

        private static void PrintAllMinions()
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Name, Age FROM Minions";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var minionName = (string)reader["Name"];
                            var minionAge = (int)reader["Age"];

                            Console.WriteLine($"{minionName} {minionAge}");
                        }
                    }
                }

                connection.Close();
            }
        }

        private static void UpdateMinions(int[] minionsIds)
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    var minionsIdsStr = string.Join(", ", minionsIds);
                    command.CommandText = $"UPDATE Minions SET Age += 1, Name = UPPER(LEFT(Name, 1)) + LOWER(RIGHT(Name, LEN(Name) - 1)) WHERE Id IN ({minionsIdsStr})";
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
