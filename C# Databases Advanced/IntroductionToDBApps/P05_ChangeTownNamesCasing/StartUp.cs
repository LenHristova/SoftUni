using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using P01_InitialSetup;

namespace P05_ChangeTownNamesCasing
{
    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Insert country's name: ");
            var countryName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(countryName))
            {
                countryName = Console.ReadLine();
                Console.Write("Please insert valid country's name: ");
            }

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.Parameters.AddWithValue("@countryName", countryName);
                    command.CommandText = "UPDATE Towns SET Name = UPPER(t.Name) FROM Towns AS t " +
                                          "JOIN Countries AS c ON c.Id = t.CountryId " +
                                          "WHERE c.Name = @countryName";

                    var rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected != 0)
                    {
                        Console.WriteLine($"{rowsAffected} town names were affected.");
                        command.CommandText = "SELECT t.Name FROM Towns AS t " +
                                              "JOIN Countries AS c ON c.Id = t.CountryId " +
                                              "WHERE c.Name = @countryName";

                        var affectedTowns = new List<string>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var townName = (string)reader["Name"];
                                affectedTowns.Add(townName);
                            }
                        }

                        Console.WriteLine($"[{string.Join(", ", affectedTowns)}]");
                    }
                    else
                    {
                        Console.WriteLine("No town names were affected.");
                    }
                }

                connection.Close();
            }
        }
    }
}
