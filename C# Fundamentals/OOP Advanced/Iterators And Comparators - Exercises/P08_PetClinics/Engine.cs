using System;
using System.Collections.Generic;
using System.Linq;

using P08_PetClinics.Models;

namespace P08_PetClinics
{
    public class Engine
    {
        private ICollection<Pet> _pets;
        private ICollection<Clinic> _clinics;

        public Engine()
        {
            _pets = new List<Pet>();
            _clinics = new List<Clinic>();
        }

        public void Run()
        {
            int commandsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < commandsCount; i++)
            {
                try
                {
                    ParseCommand(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void ParseCommand(string input)
        {
            var commandArgs = input.Split();
            var command = Enum.Parse<Command>(commandArgs[0]);
            switch (command)
            {
                case Command.Create:
                {
                    var type = commandArgs[1];
                    if (type == nameof(Pet))
                    {
                        var pet = new Pet(
                            commandArgs[2],
                            int.Parse(commandArgs[3]),
                            commandArgs[4]
                        );

                        _pets.Add(pet);
                    }
                    else if (type == nameof(Clinic))
                    {
                        var clinic = new Clinic(
                            commandArgs[2],
                            int.Parse(commandArgs[3])
                        );
                        _clinics.Add(clinic);
                    }

                    break;
                }
                case Command.Add:
                {
                    var pet = (Pet)GetByName(_pets, commandArgs[1]);
                    var clinic = (Clinic)GetByName(_clinics, commandArgs[2]);

                    Console.WriteLine(clinic.Add(pet));
                    break;
                }
                case Command.Release:
                {
                    var clinic = (Clinic)GetByName(_clinics, commandArgs[1]);
                    Console.WriteLine(clinic.Release());
                    break;
                }
                case Command.HasEmptyRooms:
                {
                    var clinic = (Clinic)GetByName(_clinics, commandArgs[1]);
                    Console.WriteLine(clinic.HasEmptyRooms);
                    break;
                }
                case Command.Print:
                {
                    var clinic = (Clinic)GetByName(_clinics, commandArgs[1]);
                    if (commandArgs.Length > 2)
                    {
                        clinic.Print(int.Parse(commandArgs[2]));
                    }
                    else
                    {
                        clinic.Print();
                    }
                    break;
                }
                default:
                    throw new NotSupportedException();
            }
        }

        private INameable GetByName(IEnumerable<INameable> collection, string name)
        {
            var nameable = collection.FirstOrDefault(p => p.Name == name);

            if (nameable == null)
            {
                throw new InvalidOperationException("Invalid Operation!");
            }

            return nameable;
        }

        private enum Command
        {
            Create,
            Add,
            Release,
            HasEmptyRooms,
            Print
        }
    }
}