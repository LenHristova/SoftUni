using System;
using System.Data.SqlClient;
using P01_InitialSetup;

namespace P02_VillainNames
{
    public class StartUp
    {
        public static void Main()
        {
            using (var connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT v.Name AS VillainName, " +
                                          "       COUNT(mv.MinionId) AS MinionsCount " +
                                          "FROM MinionsVillains AS mv " +
                                          "JOIN Villains AS v ON v.Id = mv.VillainId " +
                                          "GROUP BY v.Id, v.Name " +
                                          "HAVING COUNT(mv.MinionId) > 3 " +
                                          "ORDER BY MinionsCount DESC";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var villainName = reader["VillainName"];
                            var minionsCount = reader["MinionsCount"];
                            Console.WriteLine($"{villainName} - {minionsCount}");
                        }
                    }
                }

                connection.Close();

                //Variant #2
                //connection.Open();
                //var cmd = connection.CreateCommand();
                //cmd.CommandText = "SELECT v.Id, v.Name FROM MinionsVillains AS mv " +
                //                  "JOIN Villains AS v ON v.Id = mv.VillainId";

                //var reader = cmd.ExecuteReader();

                //var villains = new Dictionary<int, List<string>>();

                //while (reader.Read())
                //{
                //    var villainId = (int)reader["Id"];
                //    var villainName = (string)reader["Name"];
                //    if (!villains.ContainsKey(villainId))
                //    {
                //        villains.Add(villainId, new List<string>());
                //    }

                //    villains[villainId].Add(villainName);
                //}

                //var orderedVillains = villains
                //    .Where(v => v.Value.Count > 3)
                //    .OrderByDescending(v => v.Value.Count);

                //foreach (var v in orderedVillains)
                //{
                //    Console.WriteLine($"{v.Value.First()} - {v.Value.Count}");
                //}

                //connection.Close();
            }
        }
    }
}
