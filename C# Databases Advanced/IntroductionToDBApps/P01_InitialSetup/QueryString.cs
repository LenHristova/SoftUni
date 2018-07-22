using System;
using System.Collections.Generic;
using System.Text;

namespace P01_InitialSetup
{
    class QueryString
    {
        public const string CreateDatabase = "CREATE DATABASE MinionsDB";

        public const string CreateTableCountries = 
            "CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))";

        public const string CreateTableTowns = "CREATE TABLE Towns(" +
                                               "Id INT IDENTITY PRIMARY KEY," +
                                               "[Name] NVARCHAR(50) NOT NULL," +
                                               "CountryId INT FOREIGN KEY REFERENCES Countries(Id) NOT NULL)";

        public const string CreateTableMinions = "CREATE TABLE Minions(" +
                                                 "Id INT IDENTITY PRIMARY KEY," +
                                                 "[Name] NVARCHAR(30) NOT NULL," +
                                                 "Age INT NOT NULL," +
                                                 "TownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL)";

        public const string CreateTableEvilnessFactors = "CREATE TABLE EvilnessFactors(" +
                                                         "Id INT IDENTITY PRIMARY KEY," +
                                                         "Factor NVARCHAR(50) NOT NULL)";

        public const string CreateTableVillains = "CREATE TABLE Villains(" +
                                                   "Id INT IDENTITY PRIMARY KEY," +
                                                   "[Name] NVARCHAR(50) NOT NULL," +
                                                   "EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id) NOT NULL)";

        public const string CreateTableMinionsVillains = "CREATE TABLE MinionsVillains(" +
                                                         "MinionId INT FOREIGN KEY REFERENCES Minions(Id) NOT NULL," +
                                                         "VillainId INT FOREIGN KEY REFERENCES Villains(Id) NOT NULL, " +
                                                         "CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionId, VillainId))";

        public const string InsertIntoCountries = "INSERT INTO Countries " +
                                                  "VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')";

        public const string InsertIntoTowns = "INSERT INTO Towns " +
                                              "VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)";

        public const string InsertIntoMinions = "INSERT INTO Minions " +
                                                "VALUES ('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2)," +
                                                "('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)";

        public const string InsertIntoEvilnessFactors = "INSERT INTO EvilnessFactors " +
                                                        "VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')";

        public const string InsertIntoVillains = "INSERT INTO Villains " +
                                                 "VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)";

        public const string InsertIntoMinionsVillains = "INSERT INTO MinionsVillains " +
                                                        "VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";
    }
}
