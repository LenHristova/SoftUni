using System;
using System.Data.SqlClient;
using P01_InitialSetup;

namespace P04_AddMinion
{
    public class StartUp
    {
        public static void Main()
        {
            var minionInfo = GetMinionInfo();

            var minionName = minionInfo[1];
            var minionAge = int.Parse(minionInfo[2]);
            var minionTown = minionInfo[3];

            var villainName = GetVillainName();

            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    var minionId = AddMinion(minionName, minionAge, minionTown, command);
                    var villainId = EnsureVillainId(villainName, command);

                    command.Parameters.AddWithValue("@minionId", minionId);
                    command.Parameters.AddWithValue("@villainId", villainId);
                    command.CommandText = "INSERT INTO MinionsVillains VALUES(@minionId, @villainId)";

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                    }
                    catch (SqlException sqlException)
                    {
                        Console.WriteLine(sqlException.Message);
                    }
                }

                connection.Close();
            }
        }

        private static int AddMinion(string minionName, int minionAge, string minionTown, SqlCommand command)
        {
            var townId = EnsureTownId(minionTown, command);

            command.Parameters.AddWithValue("@minionName", minionName);
            command.Parameters.AddWithValue("@minionAge", minionAge);
            command.Parameters.AddWithValue("@townId", townId);

            command.CommandText = "INSERT INTO Minions VALUES (@minionName, @minionAge, @townId)";
            command.ExecuteNonQuery();

            command.CommandText = "SELECT TOP 1 Id FROM Minions WHERE Name = @minionName ORDER BY Id DESC";
            var minionId = command.ExecuteScalar();
            return (int)minionId;
        }

        private static int EnsureVillainId(string villainName, SqlCommand command)
        {
            command.Parameters.AddWithValue("@villainName", villainName);

            command.CommandText = "SELECT TOP 1 Id FROM Villains WHERE Name = @villainName";
            var villainId = command.ExecuteScalar();
            if (villainId == null)
            {
                command.CommandText = "INSERT INTO Villains SELECT @villainName, Id FROM EvilnessFactors WHERE Factor = 'Evil'";
                command.ExecuteNonQuery();

                command.CommandText = "SELECT TOP 1 Id FROM Villains WHERE  Name = @villainName";
                villainId = command.ExecuteScalar();
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }

            return (int)villainId;
        }

        private static int EnsureTownId(string townName, SqlCommand command)
        {
            command.Parameters.AddWithValue("@townName", townName);
            command.CommandText = "SELECT TOP 1 Id FROM Towns WHERE Name = @townName";
            var townId = command.ExecuteScalar();
            if (townId == null)
            {
                Console.WriteLine($"Town '{townName}' doesn't exist in the database!");
                var countryId = EnsureCountryId(command);
                command.Parameters.AddWithValue("@countryId", countryId);

                command.CommandText = "INSERT INTO Towns VALUES (@townName, @countryId)";
                command.ExecuteNonQuery();

                command.CommandText = "SELECT TOP 1 Id FROM Towns WHERE Name = @townName";
                townId = command.ExecuteScalar();
                Console.WriteLine($"Town {townName} was added to the database.");
            }

            return (int)townId;
        }

        private static int EnsureCountryId(SqlCommand command)
        {
            Console.Write($"Please add town's country's name: ");
            var countryName = Console.ReadLine();
            while (string.IsNullOrEmpty(countryName))
            {
                Console.Write("Invalid country's name! Please add town's country's name: ");
                countryName = Console.ReadLine();
            }

            command.Parameters.AddWithValue("@countryName", countryName);
            command.CommandText = "SELECT TOP 1 Id FROM Countries WHERE Name = @countryName";
            var countryId = command.ExecuteScalar();
            if (countryId == null)
            {
                command.CommandText = "INSERT INTO Countries VALUES (@countryName)";
                command.ExecuteNonQuery();

                command.CommandText = "SELECT TOP 1 Id FROM Countries WHERE Name = @countryName";
                countryId = command.ExecuteScalar();
                Console.WriteLine($"Country {countryName} was added to the database.");
            }

            return (int)countryId;
        }

        private static string GetVillainName()
        {
            string[] villainInfo;
            while (true)
            {
                Console.Write("Insert villain's name: ");
                villainInfo = Console.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                //Validate input
                if (villainInfo?.Length == 2)
                {
                    break;
                }

                Console.WriteLine("Invalid villain's info!");
            }

            return villainInfo[1];
        }

        private static string[] GetMinionInfo()
        {
            string[] minionInfo;
            while (true)
            {
                Console.Write("Insert minion's info: ");
                minionInfo = Console.ReadLine()?.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                //Validate input
                if (minionInfo?.Length == 4 && int.TryParse(minionInfo[2], out _))
                {
                    break;
                }

                Console.WriteLine("Invalid minion's info!");
            }

            return minionInfo;
        }
    }
}
