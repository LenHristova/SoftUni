namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Dtos;
    using Models;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var deserialized = JsonConvert.DeserializeObject<AnimalAidDto[]>(jsonString);

            var sb = new StringBuilder();

            var animalAids = new List<AnimalAid>();
            foreach (var dto in deserialized)
            {
                if (IsValid(dto) && animalAids.All(aa => aa.Name != dto.Name))
                {
                    var animalAid = Mapper.Map<AnimalAid>(dto);
                    animalAids.Add(animalAid);
                    sb.AppendLine($"Record {dto.Name} successfully imported.");
                }
                else
                {
                    sb.AppendLine("Error: Invalid data.");
                }
            }

            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var deserialized = JsonConvert.DeserializeObject<AnimalDto[]>(jsonString);

            var sb = new StringBuilder();

            var animals = new List<Animal>();
            var passportSerialNumbers = new List<string>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (!IsValid(dto.Passport))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (passportSerialNumbers.Any(psn => psn == dto.Passport.SerialNumber))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (!DateTime.TryParseExact(dto.Passport.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var animal = new Animal
                {
                    Name = dto.Name,
                    Passport = new Passport
                    {
                        SerialNumber = dto.Passport.SerialNumber,
                        OwnerName = dto.Passport.OwnerName,
                        OwnerPhoneNumber = dto.Passport.OwnerPhoneNumber,
                        RegistrationDate = date
                    },
                    Type = dto.Type,
                    Age = dto.Age
                };

                animals.Add(animal);
                passportSerialNumbers.Add(dto.Passport.SerialNumber);

                sb.AppendLine($"Record {dto.Name} Passport №: {dto.Passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animals);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(VetDto[]), new XmlRootAttribute("Vets"));
            var deserialized = (VetDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var phoneNumbers = new List<string>();
            var vets = new List<Vet>();
            foreach (var dto in deserialized)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                if (phoneNumbers.Contains(dto.PhoneNumber))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var vet = Mapper.Map<Vet>(dto);

                vets.Add(vet);

                phoneNumbers.Add(dto.PhoneNumber);

                sb.AppendLine($"Record {dto.Name} successfully imported.");
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ProcedureDto[]), new XmlRootAttribute("Procedures"));
            var deserialized = (ProcedureDto[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var vets = context.Vets.ToArray();
            var animals = context.Animals.ToArray();
            var animalAids = context.AnimalAids.ToArray();

            var procedureAnimalAids = new List<ProcedureAnimalAid>();
            foreach (var dto in deserialized)
            {
                if (!DateTime.TryParseExact(dto.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var vet = vets.FirstOrDefault(x => x.Name == dto.Vet);
                if (vet == null)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }


                var animal = animals.FirstOrDefault(x => x.PassportSerialNumber == dto.Animal);
                if (animal == null)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var animalAidsNames = dto.AnimalAids.Select(x => x.Name).Distinct().ToArray();
                if (animalAidsNames.Length != dto.AnimalAids.Length)
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var animalAidsToAdd = animalAidsNames
                    .Select(x => animalAids.FirstOrDefault(y => y.Name == x))
                    .ToArray();

                if (animalAidsToAdd.Any(x => x == null))
                {
                    sb.AppendLine("Error: Invalid data.");
                    continue;
                }

                var procedure = new Procedure
                {
                    Vet = vet,
                    Animal = animal,
                    DateTime = date
                };

                var currentProcedureAnimalAids = new List<ProcedureAnimalAid>();
                foreach (var aid in animalAidsToAdd)
                {
                    var currentProcedureAnimalAid = new ProcedureAnimalAid
                    {
                        Procedure = procedure,
                        AnimalAid = aid
                    };

                    currentProcedureAnimalAids.Add(currentProcedureAnimalAid);
                }

                procedureAnimalAids.AddRange(currentProcedureAnimalAids);
                sb.AppendLine("Record successfully imported.");
            }

            context.ProceduresAnimalAids.AddRange(procedureAnimalAids);
            context.SaveChanges();

            return sb.ToString().Trim();
        }

        private static bool IsValid(object obj)
            => Validator.TryValidateObject(
                obj,
                new ValidationContext(obj),
                new List<ValidationResult>(),
                true);
    }
}
