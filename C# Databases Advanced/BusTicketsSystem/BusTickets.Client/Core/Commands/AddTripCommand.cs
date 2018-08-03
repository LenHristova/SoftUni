namespace BusTickets.Client.Core.Commands
{
	using System;
	using System.Globalization;
	using Dtos;
	using Models;
	using Models.Enums;
	using Services.Contracts;

    public class AddTripCommand : Command
    {
        public AddTripCommand(IRepository repository) 
            : base(repository) { }

        public override string Execute(string[] data)
        {
            this.EnsureParametersCount(data.Length, 8);

            var departureTimeStr = data[0] + " " + data[1];
            if (!DateTime.TryParseExact(departureTimeStr, "d-M-yyyy h:m", CultureInfo.InvariantCulture, DateTimeStyles.None, out var departureTime))
            {
                throw new ArgumentException("Invalid departure time format!");
            }

            var arrivalTimeStr = data[2] + " " + data[3];
            if (!DateTime.TryParseExact(arrivalTimeStr, "d-M-yyyy h:m", CultureInfo.InvariantCulture, DateTimeStyles.None, out var arrivalTime))
            {
                throw new ArgumentException("Invalid arrival time format!");
            }

            if (!Enum.TryParse(data[4], out TripStatus status))
            {
                throw new ArgumentException("Invalid status!");
            }

            if (!int.TryParse(data[5], out var originStationId))
            {
                throw new ArgumentException("Invalid originStationId format!");
            }

            if (!int.TryParse(data[6], out var destinationStationId))
            {
                throw new ArgumentException("Invalid destinationStationId format!");
            }

            if (departureTime >= arrivalTime)
            {
                throw new ArgumentException("Departure time must be earlier then arrival time!");
            }

            var companyName = data[7];

            var originStationDto = this.repository.GetById<Station, StationBaseDto>(originStationId);
            this.EnsureNotNull(originStationDto, "Origin station", originStationId.ToString());

            var destinationStationDto = this.repository.GetById<Station, StationBaseDto>(destinationStationId);
            this.EnsureNotNull(destinationStationDto, "Destination station", destinationStationId.ToString());

            var companyDto = this.repository.Get<Company, CompanyBaseDto>(c => c.Name == companyName);
            this.EnsureNotNull(companyDto, "Company", companyName);

            var dto = new TripDto
            {
                DepartureTime = departureTime.ToString("f"),
                ArrivalTime = arrivalTime.ToString("f"),
                Status = status.ToString(),
                OriginStation = originStationDto,
                DestinationStation = destinationStationDto,
                Company = companyDto
            };

            this.repository.Add<Trip, TripDto>(dto);

            return $"Trip was was added successfully!";
        }
    }
}
