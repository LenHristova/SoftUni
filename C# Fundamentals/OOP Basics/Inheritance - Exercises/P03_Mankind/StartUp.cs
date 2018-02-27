using System;
using P03_Mankind.Models;

namespace P03_Mankind
{
    class StartUp
    {
        static void Main()
        {
            try
            {
                Student student = GetStudentInfo();

                Worker worker = GetWorkerInfo();

                Console.WriteLine(student);
                Console.WriteLine();
                Console.WriteLine(worker);
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }

        }

        private static Worker GetWorkerInfo()
        {
            var workerInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var workerFirstName = string.Empty;
            var workerLastName = string.Empty;
            var workerWeekSalary = 0M;
            var workerHoursPerDay = 0.0;

            try
            {
                workerFirstName = workerInfo[0];
                workerLastName = workerInfo[1];
                decimal.TryParse(workerInfo[2], out workerWeekSalary);
                double.TryParse(workerInfo[3], out workerHoursPerDay);
            }
            catch (IndexOutOfRangeException)
            {
            }

            var worker = new Worker(workerFirstName, workerLastName, workerWeekSalary, workerHoursPerDay);
            return worker;
        }

        private static Student GetStudentInfo()
        {
            var studentInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var studentFirstName = string.Empty;
            var studentLastName = string.Empty;
            var studentFacultyNumber = string.Empty;

            try
            {
                studentFirstName = studentInfo[0];
                studentLastName = studentInfo[1];
                studentFacultyNumber = studentInfo[2];
            }
            catch (IndexOutOfRangeException)
            {
            }

            var student = new Student(studentFirstName, studentLastName, studentFacultyNumber);
            return student;
        }
    }
}