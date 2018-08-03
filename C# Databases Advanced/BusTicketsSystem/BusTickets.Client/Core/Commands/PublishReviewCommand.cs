namespace BusTickets.Client.Core.Commands
{
	using System;
	using System.Linq;
	using Dtos;
	using Models;
	using Services.Contracts;

    public class PublishReviewCommand : Command
    {
        public PublishReviewCommand(IRepository repository) 
            : base(repository) { }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 4);

            if (!int.TryParse(data[0], out var customerId))
            {
                throw new ArgumentException("Invalid customer's id format!");
            }

            if (!double.TryParse(data[1], out var grade))
            {
                throw new ArgumentException("Invalid grade format!");
            }

            var companyName = data[2];
            var content = string.Join(" ", data.Skip(3));

            var customerDto = this.repository.GetById<Customer, CustomerDto>(customerId);
            this.EnsureNotNull(customerDto, "Customer", customerId.ToString());

            var companyDto = this.repository.Get<Company, CompanyBaseDto>(c => c.Name == companyName);
            this.EnsureNotNull(companyDto, "Company", companyName);

            var dto = new ReviewBaseDto
            {
                Company = companyDto,
                Customer = customerDto,
                Grade = grade,
                Content = content
            };

            this.repository.Add<Review, ReviewBaseDto>(dto);

            return $"Customer {customerDto.FirstName} {customerDto.LastName} published review for company {companyName}!";
        }
    }
}
