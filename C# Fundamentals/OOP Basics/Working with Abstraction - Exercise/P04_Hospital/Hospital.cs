using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_Hospital
{
    public class Hospital
    {
        private static List<Doctor> Doctors { get; } = new List<Doctor>();
        private static List<Department> Departments { get; } = new List<Department>();

        public static void DisplayInfo()
        {
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                string[] searchedInfoParams = input?.Split();
                if (searchedInfoParams == null) continue;

                string output;
                switch (searchedInfoParams.Length)
                {
                    case 1:
                    {
                        var departmentName = searchedInfoParams[0];
                        output = DisplayPatients(departmentName);
                        break;
                    }
                    case 2 when HasRoomNumber(searchedInfoParams, out int roomNumber):
                    {
                        var departmentName = searchedInfoParams[0];
                        output = DisplayPatients(departmentName, roomNumber);
                        break;
                    }
                    default:
                        var doctorName = searchedInfoParams[0] + searchedInfoParams[1];
                        output = DisplayDoctorPatients(doctorName);
                        break;
                }

                Console.WriteLine(output);
            }
        }

        private static string DisplayDoctorPatients(string doctorName)
        {
            var doctor = GetInfoForDoctor(doctorName);
            string output;
            if (doctor == null)
            {
                output = $"The doctor {doctorName} does not exists!";
            }
            else
            {
                var doctorPatients = doctor.Patients.OrderBy(p => p.Name);
                output = string.Join(Environment.NewLine, doctorPatients);
            }

            return output;
        }

        private static bool HasRoomNumber(string[] searchedInfoParams, out int room)
        {
            return int.TryParse(searchedInfoParams[1], out room);
        }

        private static string DisplayPatients(string departmentName, int roomNumber = -1)
        {
            Department department = GetInfoForDepartment(departmentName);
            string output;
            if (department == null)
            {
                output = $"The departement {departmentName} does not exists!";
            }
            else if (roomNumber == -1)
            {
                output = string.Join(Environment.NewLine, department);
            }
            else
            {
                var patientsInRoom = department.Rooms[roomNumber - 1].Beds
                    .OrderBy(p => p.Name);
                output = string.Join(Environment.NewLine, patientsInRoom);
            }

            return output;
        }

        public static void ProcessInfo()
        {
            string input;
            while ((input = Console.ReadLine()) != "Output")
            {
                UpdatePatientInfo(input);
            }
        }

        private static void UpdatePatientInfo(string input)
        {
            var patientInfo = input.Split();
            var pacient = new Patient(patientInfo[3]);

            var doctorName = patientInfo[1] + patientInfo[2];
            UpdateDoctorInfo(pacient, doctorName);

            var departmentName = patientInfo[0];
            UpdateDepartmentInfo(pacient, departmentName);
        }

        private static void UpdateDepartmentInfo(Patient pacient, string departmentName)
        {
            var department = GetInfoForDepartment(departmentName);
            if (department == null)
            {
                department = new Department(departmentName);
                Departments.Add(department);
            }

            if (department.HasFreeBed())
            {
                department.PlacePatient(pacient);
            }
        }

        private static void UpdateDoctorInfo(Patient pacient, string doctorName)
        {
            var doctor = GetInfoForDoctor(doctorName);
            if (doctor == null)
            {
                doctor = new Doctor(doctorName);
                Doctors.Add(doctor);
            }

            doctor.AddPatient(pacient);
        }

        private static Department GetInfoForDepartment(string departmentName)
        {
            return Departments.FirstOrDefault(d => d.Name == departmentName);
        }

        private static Doctor GetInfoForDoctor(string doctorName)
        {
            return Doctors.FirstOrDefault(d => d.Name == doctorName);
        }
    }
}