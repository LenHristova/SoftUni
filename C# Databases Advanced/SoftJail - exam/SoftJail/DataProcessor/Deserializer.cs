namespace SoftJail.DataProcessor
{

    using Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml.Serialization;
    using Data.Models;
    using Data.Models.Enums;
    using ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

            var departments = new List<Department>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var isValidCells = dto.Cells.All(IsValid);
                if (!isValidCells)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var department = new Department()
                {
                    Name = dto.Name,
                    Cells = dto.Cells
                        .Select(c => new Cell()
                        {
                            CellNumber = c.CellNumber,
                            HasWindow = c.HasWindow
                        })
                        .ToArray()
                };

                
                departments.Add(department);
                sb.AppendLine($"Imported {dto.Name} with {dto.Cells.Length} cells");
            }

            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var deserialized = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);

            var prisoners = new List<Prisoner>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var isValidCells = dto.Mails.All(IsValid);
                if (!isValidCells)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var incarcerationDate = DateTime.ParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime? releaseDate = null;

                if (dto.ReleaseDate != null)
                {
                    releaseDate = DateTime.ParseExact(dto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                decimal? bail = null;

                if (dto.Bail.HasValue)
                {
                    bail = dto.Bail.Value;
                }

                int? cellId = null;
                if (dto.CellId.HasValue)
                {
                    cellId = dto.CellId.Value;
                }

                var prisoner = new Prisoner()
                {
                    FullName = dto.FullName,
                    Nickname = dto.Nickname,
                    Age = dto.Age,
                    IncarcerationDate = incarcerationDate,
                    ReleaseDate = releaseDate,
                    Bail = bail,
                    CellId = cellId,
                    Mails = dto.Mails
                        .Select(m => new Mail
                        {
                            Description = m.Description,
                            Sender = m.Sender,
                            Address = m.Address
                        })
                        .ToArray()
                };


                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {dto.FullName} {dto.Age} years old");
            }

            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));
            var deserialized = (OfficerDto[])serializer.Deserialize(new StringReader(xmlString));

            var allOfficerPrisoners = new List<OfficerPrisoner>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var isValidPosition = Enum.TryParse<Position>(dto.Position, true, out var position);
                var isValidWeapon = Enum.TryParse<Weapon>(dto.Weapon, true, out var weapon);

                if (!isValidPosition || !isValidWeapon)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var officer = new Officer()
                {
                    FullName = dto.Name,
                    Salary = dto.Money,
                    Position = position,
                    Weapon = weapon,
                    DepartmentId = dto.DepartmentId
                };

                var officerPrisoners = dto.Prisoners
                    .Select(p => new OfficerPrisoner
                    {
                        PrisonerId = p.Id,
                        Officer = officer
                    })
                    .ToArray();

                allOfficerPrisoners.AddRange(officerPrisoners);

                sb.AppendLine($"Imported {dto.Name} ({dto.Prisoners.Length} prisoners)");
            }

            context.OfficersPrisoners.AddRange(allOfficerPrisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, context, results, true);

            return isValid;
        }
    }
}