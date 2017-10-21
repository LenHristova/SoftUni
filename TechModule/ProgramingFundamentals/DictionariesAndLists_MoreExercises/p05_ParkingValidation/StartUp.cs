using System;
using System.Collections.Generic;
using System.Linq;

namespace p05_ParkingValidation
{
    class User
    {
        public string Name { get; set; }
        public string License { get; set; }

        public override string ToString()
        {
            return $"{Name} => {License}";
        }
    }

    class StartUp
    {
        static void Main(string[] args)
        {
            int commandCount = int.Parse(Console.ReadLine());
            List<User> parkingUsers = new List<User>();

            for (int currCommand = 0; currCommand < commandCount; currCommand++)
            {
                string[] commandArgs = Console.ReadLine()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string command = commandArgs[0];
                switch (command)
                {
                    case "register":
                        RegisterUser(commandArgs, parkingUsers);
                        break;
                    case "unregister":
                        UnregisterUser(commandArgs, parkingUsers);
                        break;
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, parkingUsers));
        }

        private static void UnregisterUser(string[] commandArgs, List<User> parkingUsers)
        {
            string username = commandArgs[1];

            if (parkingUsers.All(user => user.Name != username))
            {
                Console.WriteLine($"ERROR: user {username} not found");
            }
            else
            {
                parkingUsers.Remove(parkingUsers.First(user => user.Name == username));
                Console.WriteLine($"user {username} unregistered successfully");
            }
            
        }

        private static void RegisterUser(string[] commandArgs, List<User> parkingUsers)
        {
            string username = commandArgs[1];

            if (parkingUsers.Any(user => user.Name == username))
            {
                string userLicensePlateNumber = parkingUsers
                    .First(user => user.Name == username).License;
                Console.WriteLine($"ERROR: already registered with plate number {userLicensePlateNumber}");
                return;
            }

            string licensePlateNumber = commandArgs[2];
            bool isValidLicense = IsValidLicense(licensePlateNumber);
            if (!isValidLicense)
            {
                Console.WriteLine($"ERROR: invalid license plate {licensePlateNumber}");
                return;
            }

            if (parkingUsers.Any(user => user.License == licensePlateNumber))
            {
                Console.WriteLine($"ERROR: license plate {licensePlateNumber} is busy");
                return;
            }

            parkingUsers.Add(new User
            {
                Name = username,
                License = licensePlateNumber
            });
            Console.WriteLine($"{username} registered {licensePlateNumber} successfully");
        }

        private static bool IsValidLicense(string licensePlateNumber)
        {
            bool isValidLength = licensePlateNumber.Length == 8;
            bool isValidFormat = IsValidFormat(licensePlateNumber);

            return isValidLength && isValidFormat;
        }

        private static bool IsValidFormat(string licensePlateNumber)
        {
            char[] licenseLetersPart = licensePlateNumber
                .Remove(2, 4)
                .ToCharArray();
            char[] licenseDigitsPart = licensePlateNumber
                .Substring(2, 4)
                .ToCharArray();

            return licenseLetersPart.All(char.IsUpper) && licenseDigitsPart.All(char.IsDigit);
        }
    }
}
