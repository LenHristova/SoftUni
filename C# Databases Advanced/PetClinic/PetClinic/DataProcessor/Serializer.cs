namespace PetClinic.DataProcessor
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Dtos;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals
                .Where(x => x.Passport.OwnerPhoneNumber == phoneNumber)
                .OrderBy(x => x.Age)
                .ThenBy(x => x.PassportSerialNumber)
                .Select(x => new
                {
                    OwnerName = x.Passport.OwnerName,
                    AnimalName = x.Name,
                    Age = x.Age,
                    SerialNumber = x.PassportSerialNumber,
                    RegisteredOn = x.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)

                })
                .ToArray();

            return JsonConvert.SerializeObject(animals, Formatting.Indented);
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var procedures = context.Procedures
                .OrderBy(x => x.DateTime)
                .ThenBy(x => x.Animal.PassportSerialNumber)
                .Select(x => new ExportProcedureDto
                {
                    Passport = x.Animal.PassportSerialNumber,
                    OwnerNumber = x.Animal.Passport.OwnerPhoneNumber,
                    DateTime = x.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                    AnimalAids = x.ProcedureAnimalAids
                        .Select(y => new AnimalAidDto
                        {
                            Name = y.AnimalAid.Name,
                            Price = y.AnimalAid.Price
                        })
                        .ToArray(),
                    TotalPrice = x.Cost
                })
                .ToArray();

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(ExportProcedureDto[]), new XmlRootAttribute("Procedures"));
            serializer.Serialize(new StringWriter(sb), procedures, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            return sb.ToString().Trim();
        }
    }
}
