namespace Employees.DtoModels
{
	using System.Collections.Generic;
	using System.Text;

    public class ManagerDto 
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> Employees { get; set; }

        public int EmployeesCount { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.FirstName} {this.LastName} | Employees: {this.EmployeesCount}");

            foreach (var employee in this.Employees)
            {
                sb.AppendLine($"  --{employee}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
