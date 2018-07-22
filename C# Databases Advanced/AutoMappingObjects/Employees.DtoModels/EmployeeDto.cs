namespace Employees.DtoModels
{
    public class EmployeeDto
    {
        public EmployeeDto(string firstName, string lastName, decimal salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Salary = salary;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"ID: {this.Id} - {this.FirstName} {this.LastName} - ${this.Salary:f2}";
        }
    }
}
