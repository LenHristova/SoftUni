namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Include(x => x.Cell)
                .Include(x => x.PrisonerOfficers)
                .ThenInclude(y => y.Officer)
                .Where(p => ids.Any(i => i == p.Id))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers
                        .OrderBy(x => x.Officer.FullName)
                        .ThenBy(x => x.PrisonerId)
                        .Select(po => new
                        {
                            OfficerName = po.Officer.FullName,
                            Department = po.Officer.Department.Name
                        })
                        .ToArray(),
                    TotalOfficerSalary = p.PrisonerOfficers.Select(po => po.Officer.Salary).Sum()

                })
                .ToArray();

            return JsonConvert.SerializeObject(prisoners, Formatting.Indented);
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var names = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries);


            var prisoners = context.Prisoners
                .Where(p => names.Contains(p.FullName))
                .OrderBy(p => p.FullName)
                .ThenBy(p => p.Id)
                .Select(p => new PrisonerExportDto()
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EncryptedMessages = p.Mails
                            .Select(m => new MessageDto()
                            {
                                Description = Reverse(m.Description)
                            })
                            .ToArray()
                })
                .ToArray();


            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(PrisonerExportDto[]), new XmlRootAttribute("Prisoners"));
            serializer.Serialize(new StringWriter(sb), prisoners, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            return sb.ToString().Trim();
        }

        private static string Reverse(string word)
        {
            return string.Join("", word.Reverse());
        }
    }
}