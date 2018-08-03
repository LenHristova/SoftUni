namespace BusTickets.Client.Core.Commands
{
    using Dtos;
    using Models;
    using Services.Contracts;

    public class AddCompanyCommand : Command
    {
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<Company> companyRepository;

        public AddCompanyCommand(IRepository<Country> countryRepository, IRepository<Company> companyRepository)
        {
            this.countryRepository = countryRepository;
            this.companyRepository = companyRepository;
        }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 2);

            var name = data[0];
            var nationality = data[1];

            var countryDto = this.countryRepository.Get<CountryDto>(c => c.Name == nationality);
            this.EnsureNotNull(countryDto, "Nationality", nationality);

            var dto = new CompanyBaseDto
            {
                Name = name,
                Nationality = countryDto
            };

            this.companyRepository.Add(dto);

            return $"Company {name} was added successfully!";
        }
    }
}
