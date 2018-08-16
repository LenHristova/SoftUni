using System;
using Stations.Data;

namespace Stations.DataProcessor
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Dto.Export;
    using Models.Enums;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
	{
		public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
		{
		    var date = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
		    var trains = context.Trains
		        .Where(t => t.Trips.Any(tr => tr.Status == TripStatus.Delayed && tr.DepartureTime <= date))
		        .Select(t => new
		        {
		            TrainNumber = t.TrainNumber,
		            DelayedTimes = t.Trips.Count(tr => tr.Status == TripStatus.Delayed),
		            MaxDelayedTime = t.Trips.Max(tr => tr.TimeDifference ?? new TimeSpan(0, 0, 0))
		        })
		        .OrderByDescending(t => t.DelayedTimes)
		        .ThenByDescending(t => t.MaxDelayedTime)
		        .ThenBy(t => t.TrainNumber)
		        .ToArray();

		    return JsonConvert.SerializeObject(trains, Formatting.Indented);
		}

		public static string ExportCardsTicket(StationsDbContext context, string cardType)
		{
		    var type = Enum.Parse<CardType>(cardType);

		    var cards = context.Cards
		        .Where(c => c.Type == type && c.BoughtTickets.Any())
		        .Select(c => new CardDto
		        {
		            Name = c.Name,
		            Type = c.Type.ToString(),
		            Tickets = c.BoughtTickets
		                .Select(t => new TicketDto
		                {
		                    OriginStation = t.Trip.OriginStation.Name,
		                    DestinationStation = t.Trip.DestinationStation.Name,
		                    DepartureTime =
		                        t.Trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
		                })
		                .ToArray()
		        })
		        .OrderBy(c => c.Name)
		        .ToArray();

		    var sb = new StringBuilder();
		    var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            serializer.Serialize(new StringWriter(sb), cards, new XmlSerializerNamespaces(new []{XmlQualifiedName.Empty}));
		    return sb.ToString();
		}
	}
}