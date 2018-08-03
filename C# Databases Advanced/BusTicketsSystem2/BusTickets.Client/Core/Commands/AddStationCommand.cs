namespace BusTickets.Client.Core.Commands
{
	using Dtos;
	using Models;
	using Services.Contracts;

    public class AddStationCommand : Command
    {
        private readonly IRepository<Town> townRepository;
        private readonly IRepository<Station> stationRepository;

        public AddStationCommand(IRepository<Town> townRepository, IRepository<Station> stationRepository)
        {
            this.townRepository = townRepository;
            this.stationRepository = stationRepository;
        }


        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 1);

            var name = data[0];
            var townName = data[1];

            var townDto = this.townRepository.Get<TownBaseDto>(t => t.Name == townName);
            this.EnsureNotNull(townDto, "Town", townName);

            var dto = new StationBaseDto
            {
                Name = name,
                Town = townDto
            };

            this.stationRepository.Add(dto);

            return $"Station {name} was added successfully!";
        }
    }
}
