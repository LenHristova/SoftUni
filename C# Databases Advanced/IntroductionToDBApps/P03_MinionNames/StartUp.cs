using System;
using System.Data.SqlClient;
using P01_InitialSetup;

namespace P03_MinionNames
{
    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Enter villain's id: ");
            var vilainId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@vilainId", vilainId);

                    var villainName = FindVillainNameById(command);

                    if (villainName != null)
                    {
                        Console.WriteLine($"Villain: {villainName}");
                        PrintVillainMinions(command);
                    }
                    else
                    {
                        Console.WriteLine($"No villain with ID {vilainId} exists in the database.");
                    }
                }

                connection.Close();
            }
        }

        private static void PrintVillainMinions(SqlCommand command)
        {
            command.CommandText = "SELECT m.Name, m.Age FROM Minions AS m " +
                                  "JOIN MinionsVillains AS mv ON mv.MinionId = m.Id " +
                                  "WHERE mv.VillainId = @vilainId " +
                                  "ORDER BY m.Name";

            using (var reader = command.ExecuteReader())
            {
                if (!reader.HasRows)
                {
                    Console.WriteLine("(no minions)");
                }
                else
                {
                    var counter = 1;
                    while (reader.Read())
                    {
                        var minionName = (string)reader["Name"];
                        var minionAge = (int)reader["Age"];
                        Console.WriteLine($"{counter}. {minionName} {minionAge}");
                        counter++;
                    }
                }
            }
        }

        private static string FindVillainNameById(SqlCommand command)
        {
            command.CommandText = "SELECT Name FROM Villains WHERE Id = @vilainId";

            string villainName;
            using (var reader = command.ExecuteReader())
            {
                 villainName = reader.Read() ? (string)reader["Name"] : null;
            }

            return villainName;
        }
    }
}
