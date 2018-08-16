using System;
using Stations.Data;

namespace Stations.DataProcessor
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Dto.Import;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportStations(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<Station[]>(jsonString);

            var stations = new List<Station>();
            foreach (var station in deserialized)
            {
                if (station.Town == null)
                {
                    station.Town = station.Name;
                }

                if (!IsValid(station))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var hasDuplicate = stations.Any(s => s.Name == station.Name);

                if (hasDuplicate)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                stations.Add(station);
                sb.AppendLine(string.Format(SuccessMessage, station.Name));
            }

            context.Stations.AddRange(stations);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportClasses(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<SeatingClass[]>(jsonString);

            var classes = new List<SeatingClass>();
            foreach (var @class in deserialized)
            {
                if (!IsValid(@class))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var hasDuplicate = classes.Any(s => s.Name == @class.Name || s.Abbreviation == @class.Abbreviation);

                if (hasDuplicate)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                classes.Add(@class);
                sb.AppendLine(string.Format(SuccessMessage, @class.Name));
            }

            context.SeatingClasses.AddRange(classes);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTrains(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<TrainDto[]>(jsonString, 
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var trains = new List<Train>();
            var trainSeats = new List<TrainSeat>();
            foreach (var trainDto in deserialized)
            {
                if (!IsValid(trainDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var hasDuplicate = trains.Any(t => t.TrainNumber == trainDto.TrainNumber);
                if (hasDuplicate)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (trainDto.Seats.Any(s => !IsValid(s)))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var classes = trainDto.Seats
                    .Select(s => context.SeatingClasses
                        .SingleOrDefault(cl => cl.Name == s.Name && cl.Abbreviation == s.Abbreviation))
                    .ToArray();

                var invalidClasses = classes.Any(cl => cl == null);
                if (invalidClasses)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var type = Enum.Parse<TrainType>(trainDto.Type);

                var train = new Train()
                {
                    TrainNumber = trainDto.TrainNumber,
                    Type = type
                };

                foreach (var dtoSeat in trainDto.Seats)
                {
                    var seatClass = classes.Single(cl => cl.Name == dtoSeat.Name);

                    var seat = new TrainSeat()
                    {
                        Train = train,
                        SeatingClass = seatClass,
                        Quantity = dtoSeat.Quantity.Value
                    };

                    trainSeats.Add(seat);
                }

                trains.Add(train);
                sb.AppendLine(string.Format(SuccessMessage, train.TrainNumber));
            }

            context.Trains.AddRange(trains);
            context.SaveChanges();

            context.TrainSeats.AddRange(trainSeats);
            context.SaveChanges();

            return sb.ToString().TrimEnd();

        }

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<TripDto[]>(jsonString);

            var trips = new List<Trip>();
            foreach (var tripDto in deserialized)
            {
                if (!IsValid(tripDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var train = context.Trains.SingleOrDefault(t => t.TrainNumber == tripDto.Train);
                if (train == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var originStation = context.Stations.SingleOrDefault(t => t.Name == tripDto.OriginStation);
                var destinationStation = context.Stations.SingleOrDefault(t => t.Name == tripDto.DestinationStation);
                if (originStation == null || destinationStation == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var isValidDepartureTime = DateTime.TryParseExact(tripDto.DepartureTime, "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var departureTime);
                var isValidArrivalTime = DateTime.TryParseExact(tripDto.ArrivalTime, "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var arrivalTime);

                if (!isValidDepartureTime ||
                    !isValidArrivalTime ||
                    departureTime >= arrivalTime)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var status = tripDto.Status == null
                    ? TripStatus.OnTime
                    : Enum.Parse<TripStatus>(tripDto.Status);

                TimeSpan? timeDifference = null;

                if (tripDto.TimeDifference != null)
                {
                    timeDifference =
                        TimeSpan.ParseExact(tripDto.TimeDifference, "hh\\:mm", CultureInfo.InvariantCulture);
                }

                var trip = new Trip()
                {
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destinationStation,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    Status = status,
                    TimeDifference = timeDifference
                };

                trips.Add(trip);
                sb.AppendLine($"Trip from {tripDto.OriginStation} to {tripDto.DestinationStation} imported.");
            }

            context.Trips.AddRange(trips);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            var deserialized = (CardDto[])serializer.Deserialize(new StringReader(xmlString));

            var cards = new List<CustomerCard>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var type = dto.Type == null
                    ? CardType.Normal
                    : Enum.Parse<CardType>(dto.Type);

                var card = new CustomerCard()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Type = type
                };

                cards.Add(card);
                sb.AppendLine(string.Format(SuccessMessage, card.Name));
            }

            context.Cards.AddRange(cards);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(TicketDto[]), new XmlRootAttribute("Tickets"));
            var deserialized = (TicketDto[])serializer.Deserialize(new StringReader(xmlString));

            var tickets = new List<Ticket>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!IsValid(dto.Trip))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (dto.Card != null && !IsValid(dto.Card))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                CustomerCard card = null;
                if (dto.Card != null)
                {
                    card = context.Cards.SingleOrDefault(c => c.Name == dto.Card.Name);
                    if (card == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }
                }

                var time = DateTime.ParseExact(dto.Trip.DepartureTime, "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture);

                var trip = context.Trips
                    .SingleOrDefault(t => t.OriginStation.Name == dto.Trip.OriginStation &&
                                          t.DestinationStation.Name == dto.Trip.DestinationStation &&
                                          t.DepartureTime == time);

                if (trip == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var placeAbbreviation = dto.Seat.Substring(0, 2);
                var quantity = int.Parse(dto.Seat.Substring(2));

                var isValidSeatingPlace = trip.Train.TrainSeats
                    .Any(s => s.SeatingClass.Abbreviation == placeAbbreviation &&
                              s.Quantity >= quantity);

                if (!isValidSeatingPlace)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var ticket = new Ticket()
                {
                    Trip = trip,
                    Price = dto.Price,
                    SeatingPlace = dto.Seat,
                    CustomerCard = card
                };

                tickets.Add(ticket);
                sb.AppendLine(string.Format("Ticket from {0} to {1} departing at {2} imported.",
                    trip.OriginStation.Name,
                    trip.DestinationStation.Name,
                    trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)));
            }

            context.Tickets.AddRange(tickets);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, context, results, true);

            return isValid;
        }
    }
}