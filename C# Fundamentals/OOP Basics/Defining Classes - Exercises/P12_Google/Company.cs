namespace P12_Google
{
    using System;

    public class Company
    {
        private string _name;
        private string _department;
        private decimal _salary;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }

        public decimal Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        public Company()
        {
        }

        public Company(string name, string department, decimal salary)
        {
            _name = name;
            _department = department;
            _salary = salary;
        }

        public override string ToString()
        {
            return Name == null || Department == null 
                ? string.Empty
                : $"{Environment.NewLine}{Name} {Department} {Salary:F2}";
        }
    }
}