namespace BusTickets.Client.Core.Commands
{
	using System;
	using Dtos;
	using Models;
	using Services.Contracts;

    public class AddTownCommand : Command
    {
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<Town> townRepository;

        public AddTownCommand(IRepository<Country> countryRepository, IRepository<Town> townRepository)
        {
            this.countryRepository = countryRepository;
            this.townRepository = townRepository;
        }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 2);

            var name = data[0];
            var countryName = data[1];

            var countryDto = this.countryRepository.Get<CountryDto>(t => t.Name == countryName);
            this.EnsureNotNull(countryDto, "Country", countryName);

            var dto = new TownBaseDto()
            {
                Name = name,
                Country = countryDto
            };

            this.townRepository.Add(dto);

            return $"Town {name} was added successfully!";
        }
    }
}
