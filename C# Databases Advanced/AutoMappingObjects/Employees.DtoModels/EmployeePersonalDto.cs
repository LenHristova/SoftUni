namespace Employees.DtoModels
{
	using System;
		
    public class EmployeePersonalDto 
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string Birthday { get; set; }

        public string Address { get; set; }

        public override string ToString()
        {
            var birthday = this.Birthday ?? "[N/A]";
            var address = this.Address ?? "[N/A]";

            return $"ID: {this.Id} - {this.FirstName} {this.LastName} - ${this.Salary:f2}{Environment.NewLine}" +
                   $"Birthday: {birthday}{Environment.NewLine}" +
                   $"Address: {address}";
        }
    }
}
