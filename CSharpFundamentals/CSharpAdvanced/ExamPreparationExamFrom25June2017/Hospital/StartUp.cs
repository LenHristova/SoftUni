using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Hospital
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var departmentsPatients = new Dictionary<string, List<string>>();
            var doctorsPatients = new Dictionary<string, List<string>>();

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Output")
                    break;

                var info = input
                    .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);

                var department = info[0];
                var doctor = info[1] + " " + info[2];
                var patient = info[3];

                if (!departmentsPatients.ContainsKey(department))
                {
                    departmentsPatients.Add(department, new List<string>());
                }

                if (departmentsPatients[department].Count < 60)
                {
                    departmentsPatients[department].Add(patient);
                }

                if (!doctorsPatients.ContainsKey(doctor))
                {
                    doctorsPatients.Add(doctor, new List<string>());
                }

                doctorsPatients[doctor].Add(patient);
            }

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "End")
                    break;

                var commandArgs = input
                    .Split(new[] {' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);

                if (commandArgs.Length == 1)
                {
                    var department = commandArgs[0];
                    PrintDepartmentPatientsInfo(department, departmentsPatients);
                }
                else if (int.TryParse(commandArgs[1], out int room))
                {
                    var department = commandArgs[0];
                    PrintDepartmentRoomInfo(department, room, departmentsPatients);
                }
                else
                {
                    var doctor = input;
                    PrintDoctorPatientsInfo(doctor, doctorsPatients);
                }
            }
        }

        private static void PrintDoctorPatientsInfo(string doctor, Dictionary<string, List<string>> doctorsPatients)
        {
            foreach (var patient in doctorsPatients[doctor]
            .OrderBy(p => p))
            {
                Console.WriteLine(patient);
            }
        }

        private static void PrintDepartmentRoomInfo(string department, int room, Dictionary<string, List<string>> departmentsPatients)
        {
            var patientsInPreviousRoom = 3 * (room - 1);
            var patientsInRoom = departmentsPatients[department]
                .Skip(patientsInPreviousRoom)
                .Take(3)
                .OrderBy(p => p)
                .ToList();

            Console.WriteLine(string.Join(Environment.NewLine, patientsInRoom));
        }

        private static void PrintDepartmentPatientsInfo(string department, Dictionary<string, List<string>> departmentsPatients)
        {
            Console.WriteLine(string.Join(Environment.NewLine, departmentsPatients[department]));
        }
    }
}