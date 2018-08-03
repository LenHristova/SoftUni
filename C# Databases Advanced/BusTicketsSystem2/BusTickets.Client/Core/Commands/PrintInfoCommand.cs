namespace BusTickets.Client.Core.Commands
{
    using System;
    using System.Text;
    using Dtos;
    using Models;
    using Services.Contracts;

    public class PrintInfoCommand : Command
    {
        private readonly IRepository<Station> stationRepository;

        public PrintInfoCommand(IRepository<Station> stationRepository)
        {
            this.stationRepository = stationRepository;
        }

        public override string Execute(string[] data)
        {
            EnsureParametersCount(data.Length, 1);

            if (!int.TryParse(data[0], out var stationId))
            {
                throw new ArgumentException("Invalid station's id format!");
            }

            var stationDto = this.stationRepository.GetById<StationDto>(stationId);
            this.EnsureNotNull(stationDto, "Station", stationId.ToString());

            return GetStationInfo(stationDto);
        }

        private static string GetStationInfo(StationDto stationDto)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{stationDto.Name}, {stationDto.Town.Name}")
                .AppendLine("Arivals:");

            foreach (var s in stationDto.TripDestinationStations)
            {
                sb.AppendLine($"From {s.OriginStation.Name} | Arrive at: {s.ArrivalTime} | Status: {s.Status}");
            }

            sb.AppendLine("Departures:");
            foreach (var s in stationDto.TripOriginStations)
            {
                sb.AppendLine($"To {s.DestinationStation.Name} | Depart at: {s.DepartureTime} | Status: {s.Status}");
            }

            return sb.ToString().Trim();
        }
    }
}
