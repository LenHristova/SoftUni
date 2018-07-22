using System;
using System.Data;
using System.Data.SqlClient;

namespace P01_InitialSetup
{
    public class StartUp
    {
        public static void Main()
        {
            using (var connection = new SqlConnection(Configuration.ConnectionStringInit))
            {
                connection.Open();

                try
                {
                    var command = connection.CreateCommand();
                    EnsureDatabase(command);

                    connection.ChangeDatabase("MinionsDB");

                    EnsureTable(command, QueryString.CreateTableCountries, "Countries");
                    EnsureTable(command, QueryString.CreateTableTowns, "Towns");
                    EnsureTable(command, QueryString.CreateTableMinions, "Minions");
                    EnsureTable(command, QueryString.CreateTableEvilnessFactors, "EvilnessFactors");
                    EnsureTable(command, QueryString.CreateTableVillains, "Villains");
                    EnsureTable(command, QueryString.CreateTableMinionsVillains, "MinionsVillains");

                    Seed(command, QueryString.InsertIntoCountries);
                    Seed(command, QueryString.InsertIntoTowns);
                    Seed(command, QueryString.InsertIntoMinions);
                    Seed(command, QueryString.InsertIntoEvilnessFactors);
                    Seed(command, QueryString.InsertIntoVillains);
                    Seed(command, QueryString.InsertIntoMinionsVillains);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                connection.Close();
            }
        }

        private static void EnsureDatabase(IDbCommand command)
        {
            var databaseAlreadyExistExceptionNumber = 1801;

            try
            {
                command.CommandText = QueryString.CreateDatabase;
                command.ExecuteNonQuery();
                Console.WriteLine("Database 'MinionsDB' created!");
            }
            catch (SqlException sqlException)
            {
                if (sqlException.Number == databaseAlreadyExistExceptionNumber)
                {
                    Console.WriteLine("Database 'MinionsDB' already exists!");
                }
                else
                {
                    throw;
                }
            }
        }

        private static void EnsureTable(IDbCommand command, string query, string tableName)
        {
            var tableAlreadyCreatedExceptionNumber = 2714;
            try
            {
                command.CommandText = query;
                command.ExecuteNonQuery();
                Console.WriteLine($"Table '{tableName}' created!");
            }
            catch (SqlException sqlException)
            {
                if (sqlException.Number == tableAlreadyCreatedExceptionNumber)
                {
                    Console.WriteLine($"Table '{tableName}' already exists!");
                }
                else
                {
                    throw;
                }
            }
        }

        private static void Seed(IDbCommand command, string query)
        {
            command.CommandText = query;
            command.ExecuteNonQuery();
        }
    }
}
