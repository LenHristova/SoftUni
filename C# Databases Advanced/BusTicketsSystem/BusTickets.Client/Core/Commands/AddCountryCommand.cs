namespace BusTickets.Client.Core.Commands
{
    using Dtos;
	using Models;
	using Services;
    using Services.Contracts;

    public class AddCountryCommand : Command
    {
        public AddCountryCommand(IRepository repository) 
            : base(repository) { }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 1);

            var name = data[0];

            var dto = new CountryDto {Name = name};

            this.repository.Add<Country, CountryDto>(dto);

            return $"Country {name} was added successfully!";
        }
    }
}
