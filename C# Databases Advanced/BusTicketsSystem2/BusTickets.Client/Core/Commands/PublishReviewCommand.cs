namespace BusTickets.Client.Core.Commands
{
	using System;
	using System.Linq;
	using Dtos;
	using Models;
	using Services.Contracts;

    public class PublishReviewCommand : Command
    {
        private readonly IRepository<Customer> customerRepository;
        private readonly IRepository<Company> companyRepository;
        private readonly IRepository<Review> reviewRepository;

        public PublishReviewCommand(IRepository<Customer> customerRepository, IRepository<Company> companyRepository, IRepository<Review> reviewRepository)
        {
            this.customerRepository = customerRepository;
            this.companyRepository = companyRepository;
            this.reviewRepository = reviewRepository;
        }

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

            var customerDto = this.customerRepository.GetById<CustomerDto>(customerId);
            this.EnsureNotNull(customerDto, "Customer", customerId.ToString());

            var companyDto = this.companyRepository.Get<CompanyBaseDto>(c => c.Name == companyName);
            this.EnsureNotNull(companyDto, "Company", companyName);

            var dto = new ReviewBaseDto
            {
                Company = companyDto,
                Customer = customerDto,
                Grade = grade,
                Content = content
            };

            this.reviewRepository.Add(dto);

            return $"Customer {customerDto.FirstName} {customerDto.LastName} published review for company {companyName}!";
        }
    }
}
