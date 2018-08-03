namespace BusTickets.Client.Core.Commands
{
    using Dtos;
	using Models;
	using Services;
    using Services.Contracts;

    public class AddCountryCommand : Command
    {
        private readonly IRepository<Country> countryRepository;

        public AddCountryCommand(IRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 1);

            var name = data[0];

            var dto = new CountryDto {Name = name};

            this.countryRepository.Add(dto);

            return $"Country {name} was added successfully!";
        }
    }
}
