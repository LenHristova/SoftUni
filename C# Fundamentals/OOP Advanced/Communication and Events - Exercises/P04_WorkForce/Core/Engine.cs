using P04_WorkForce.Contracts;
using P04_WorkForce.Models.Jobs;

namespace P04_WorkForce.Core
{
    public class Engine
    {
        private readonly ITaskManager _taskManager;
        private readonly IEmployeeManager _employeeManager;
        private readonly IReader _reader;

        public Engine(ITaskManager taskManager, IEmployeeManager employeeManager, IReader reader)
        {
            _taskManager = taskManager;
            _employeeManager = employeeManager;
            _reader = reader;
        }

        public void Run()
        {
            string input;
            while ((input = _reader.ReadLine()) != "End")
            {
                var commandArgs = input?.Split();
                var command = commandArgs?[0];

                if (command != null && command.EndsWith("Employee"))
                {
                    _employeeManager.CreateEmployee(commandArgs);
                }
                else if(command == nameof(Job))
                {
                    var name = commandArgs[1];
                    var requaredHours = int.Parse(commandArgs[2]);

                    var employeeName = commandArgs[3];
                    var employee = _employeeManager.GetEmployee(employeeName);
                    _taskManager.AddNewTask(name, requaredHours, employee);
                }
                else if (command == "Status")
                {
                    _taskManager.PrintStatus();
                }
                else if (input == "Pass Week")
                {
                    _taskManager.WeekPass();
                }
            }
        }
    }
}