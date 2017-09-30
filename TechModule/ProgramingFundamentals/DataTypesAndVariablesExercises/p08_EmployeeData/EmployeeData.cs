using System;

namespace p08_EmployeeData
{
    class EmployeeData
    {
        static void Main(string[] args)
        {
            string firstName = "Amanda";
            string lastName = "Jonson";
            byte age = 27;
            char gender = 'f';
            long personalID = 8306112507L;
            int employeeNumber = 27563571;

            Console.WriteLine(
                $"First name: {firstName}\r\n" +
                $"Last name: {lastName}\r\n" +
                $"Age: {age}\r\n" +
                $"Gender: {gender}\r\n" +
                $"Personal ID: {personalID}\r\n" +
                $"Unique Employee number: {employeeNumber}");
        }
    }
}
