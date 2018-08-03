namespace BusTickets.Client.Core.Commands
{
	using System;
	using Dtos;
	using Models;
	using Services.Contracts;

    public class AddTownCommand : Command
    {
        public AddTownCommand(IRepository repository)
            : base(repository) { }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 2);

            var name = data[0];
            var countryName = data[1];

            var countryDto = this.repository.Get<Country, CountryDto>(t => t.Name == countryName);
            this.EnsureNotNull(countryDto, "Country", countryName);

            var dto = new TownBaseDto()
            {
                Name = name,
                Country = countryDto
            };

            this.repository.Add<Town, TownBaseDto>(dto);

            return $"Town {name} was added successfully!";
        }
    }
}
