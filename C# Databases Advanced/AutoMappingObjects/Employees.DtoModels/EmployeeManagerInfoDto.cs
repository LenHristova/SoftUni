namespace Employees.DtoModels
{
    public class EmployeeManagerInfoDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string ManagerLastName { get; set; }

        public override string ToString()
        {
            var manager = ManagerLastName ?? "[No manager]";
            return $"{this.FirstName} {this.LastName} - ${this.Salary:F2} - Manager: {manager}";
        }
    }
}
