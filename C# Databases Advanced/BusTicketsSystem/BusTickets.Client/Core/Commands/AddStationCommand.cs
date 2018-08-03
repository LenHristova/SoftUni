namespace BusTickets.Client.Core.Commands
{
	using Dtos;
	using Models;
	using Services.Contracts;

    public class AddStationCommand : Command
    {
        public AddStationCommand(IRepository repository) 
            : base(repository) { }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 1);

            var name = data[0];
            var townName = data[1];

            var townDto = this.repository.Get<Town, TownBaseDto>(t => t.Name == townName);
            this.EnsureNotNull(townDto, "Town", townName);

            var dto = new StationBaseDto
            {
                Name = name,
                Town = townDto
            };

            this.repository.Add<Station, StationBaseDto>(dto);

            return $"Station {name} was added successfully!";
        }
    }
}
