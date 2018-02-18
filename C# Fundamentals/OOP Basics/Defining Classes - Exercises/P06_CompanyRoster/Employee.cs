namespace P06_CompanyRoster
{
    public class Employee
    {
        private string _name;
        private decimal _salary;
        private string _position;
        private string _department;
        private string _email;
        private int _age;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public decimal Salary
        {
            get => _salary;
            set => _salary = value;
        }

        public string Position
        {
            get => _position;
            set => _position = value;
        }

        public string Department
        {
            get => _department;
            set => _department = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public int Age
        {
            get => _age;
            set => _age = value;
        }

        public Employee()
        {
            _email = "n/a";
            _age = -1;
        }

        public Employee(string name, decimal salary, string position, string department) : this()
        {
            _name = name;
            _salary = salary;
            _position = position;
            _department = department;
        }

        public override string ToString()
        {
            return $"{Name} {Salary:F2} {Email} {Age}";
        }
    }
}