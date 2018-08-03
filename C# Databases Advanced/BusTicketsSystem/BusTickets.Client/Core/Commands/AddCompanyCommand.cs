namespace BusTickets.Client.Core.Commands
{
    using System;
    using Dtos;
    using Models;
    using Services.Contracts;

    public class AddCompanyCommand : Command
    {
        private readonly ICountryService countryService;
        private readonly ICompanyService companyService;

        public AddCompanyCommand(ICountryService countryService, ICompanyService companyService)
        {
            this.countryService = countryService;
            this.companyService = companyService;
        }


        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 2);

            var name = data[0];
            var nationality = data[1];

            var countryDto = this.countryService.Get<CountryDto>(c => c.Name == nationality);
            this.EnsureNotNull(countryDto, "Nationality", nationality);

            var dto = new CompanyBaseDto
            {
                Name = name,
                Nationality = countryDto
            };

            this.repository.Add<Company, CompanyBaseDto>(dto);

            return $"Company {name} was added successfully!";
        }
    }
}
