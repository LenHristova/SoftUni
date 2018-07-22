using System;
using System.Data.SqlClient;
using P01_InitialSetup;

namespace P09_IncreaseAgeStoredProcedure
{
    public class StartUp
    {
        public static void Main()
        {
            var minionId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {                   
                    command.Parameters.AddWithValue("@minionId", minionId);
                    command.CommandText = "EXEC usp_GetOlder @minionId";
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT Name, Age FROM Minions WHERE Id = @minionId";
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();
                        var minionName = (string)reader["Name"];
                        var minionAge = (int)reader["Age"];

                        Console.WriteLine($"{minionName} – {minionAge}-years old");
                    }
                }

                connection.Close();
            }
        }
    }
}
