using System;
using System.Data.SqlClient;
using P01_InitialSetup;

namespace P06_Remove_Villain
{
    public class StartUp
    {
        public static void Main()
        {
            var villainId = int.Parse(Console.ReadLine());

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = connection.BeginTransaction();

                    command.Parameters.AddWithValue("@villainId", villainId);
                    command.CommandText = "SELECT TOP 1 Name FROM Villains WHERE Id = @villainId";
                    var villainName = command.ExecuteScalar();

                    if (villainName == null)
                    {
                        Console.WriteLine("No such villain was found.");
                    }
                    else
                    {
                        try
                        {
                            var releasedMinions = RemoveVillain(command);

                            Console.WriteLine($"{villainName} was deleted.");
                            Console.WriteLine($"{releasedMinions} minions were released.");
                            command.Transaction.Commit();
                        }
                        catch (SqlException)
                        {
                            command.Transaction.Rollback();
                        }
                    }
                }

                connection.Close();
            }
        }

        private static int RemoveVillain(SqlCommand command)
        {
            command.CommandText = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";
            var rowsAffected = command.ExecuteNonQuery();

            command.CommandText = "DELETE FROM Villains WHERE Id = @villainId";
            command.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}
