using System;
using System.Collections.Generic;
using System.Linq;

namespace p03_Jarvis
{
    class Arm
    {
        public int EnergyConsumption { get; set; }
        public int ReachDistance { get; set; }
        public int FingersCount { get; set; }

        public override string ToString()
        {
            return $"#Arm: {Environment.NewLine}" +
                   $"###Energy consumption: {EnergyConsumption}{Environment.NewLine}" +
                   $"###Reach: {ReachDistance}{Environment.NewLine}" +
                   $"###Fingers: {FingersCount}";
        }
    }

    class Leg
    {
        public int EnergyConsumption { get; set; } = 0;
        public int Strength { get; set; }
        public int Speed { get; set; }

        public override string ToString()
        {
            return $"#Leg: {Environment.NewLine}" +
                   $"###Energy consumption: {EnergyConsumption}{Environment.NewLine}" +
                   $"###Strength: {Strength}{Environment.NewLine}" +
                   $"###Speed: {Speed}";
        }
    }

    class Torso
    {
        public int EnergyConsumption { get; set; }
        public double ProcessorSize { get; set; }
        public string Material { get; set; }

        public override string ToString()
        {
            return $"#Torso: {Environment.NewLine}" +
                   $"###Energy consumption: {EnergyConsumption}{Environment.NewLine}" +
                   $"###Processor size: {ProcessorSize:F1}{Environment.NewLine}" +
                   $"###Corpus material: {Material}";
        }
    }

    class Head
    {
        public int EnergyConsumption { get; set; }
        public int Iq { get; set; }
        public string Material { get; set; }

        public override string ToString()
        {
            return $"#Head: {Environment.NewLine}" +
                   $"###Energy consumption: {EnergyConsumption}{Environment.NewLine}" +
                   $"###IQ: {Iq}{Environment.NewLine}" +
                   $"###Skin material: {Material}";
        }
    }

    class Jarvis
    {
        public List<Arm> Arms { get; set; } = new List<Arm>();
        public List<Leg> Legs { get; set; } = new List<Leg>();
        public Torso Torso { get; set; }
        public Head Head { get; set; }
        public long EnergyCapacity { get; set; }

        public override string ToString()
        {
            return $"Jarvis:{Environment.NewLine}" +
                   $"{Head}{Environment.NewLine}" +
                   $"{Torso}{Environment.NewLine}" +
                   $"{string.Join(Environment.NewLine, Arms.OrderBy(arm => arm.EnergyConsumption))}{Environment.NewLine}" +
                   $"{string.Join(Environment.NewLine, Legs.OrderBy(leg => leg.EnergyConsumption))}";
        }

        public bool HasAllParts()
        {
            return Arms.Count == 2 &&
                   Legs.Count == 2 &&
                   Head != null &&
                   Torso != null;
        }

        public bool HasEnergyCapacity()
        {
            long neededEnergy = (long)Arms[0].EnergyConsumption + Arms[1].EnergyConsumption +
                                Legs[0].EnergyConsumption + Legs[1].EnergyConsumption +
                               Head.EnergyConsumption +
                               Torso.EnergyConsumption;
            return EnergyCapacity >= neededEnergy;
        }
    }

    class StartUp
    {
        static void Main(string[] args)
        {
            Jarvis jarvis = new Jarvis
            {
                EnergyCapacity = long.Parse(Console.ReadLine())
            };

            AssembleJarvis(jarvis);

            if (!jarvis.HasAllParts())
            {
                Console.WriteLine("We need more parts!");
            }
            else if (!jarvis.HasEnergyCapacity())
            {
                Console.WriteLine("We need more power!");
            }
            else
            {
                Console.WriteLine(jarvis);
            }
        }

        private static void AssembleJarvis(Jarvis jarvis)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "Assemble!")
                    break;

                string[] componentArgs = input
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string componentType = componentArgs[0];
                switch (componentType)
                {
                    case "Arm":
                        AddArm(componentArgs, jarvis);
                        break;
                    case "Leg":
                        AddLeg(componentArgs, jarvis);
                        break;
                    case "Torso":
                        AddTorso(componentArgs, jarvis);
                        break;
                    case "Head":
                        AddHead(componentArgs, jarvis);
                        break;
                }
            }
        }

        private static void AddHead(string[] componentArgs, Jarvis jarvis)
        {
            int energyConsumption = int.Parse(componentArgs[1]);

            if (jarvis.Head == null)
            {
                jarvis.Head = new Head
                {
                    EnergyConsumption = energyConsumption,
                    Iq = int.Parse(componentArgs[2]),
                    Material = componentArgs[3]
                };
            }
            else if (jarvis.Head.EnergyConsumption > energyConsumption)
            {
                jarvis.Head = new Head
                {
                    EnergyConsumption = energyConsumption,
                    Iq = int.Parse(componentArgs[2]),
                    Material = componentArgs[3]
                };
            }
        }

        private static void AddTorso(string[] componentArgs, Jarvis jarvis)
        {
            int energyConsumption = int.Parse(componentArgs[1]);
            if (jarvis.Torso == null)
            {
                jarvis.Torso = new Torso
                {
                    EnergyConsumption = energyConsumption,
                    ProcessorSize = double.Parse(componentArgs[2]),
                    Material = componentArgs[3]
                };
            }
            else if (jarvis.Torso.EnergyConsumption > energyConsumption)
            {
                jarvis.Torso = new Torso
                {
                    EnergyConsumption = energyConsumption,
                    ProcessorSize = double.Parse(componentArgs[2]),
                    Material = componentArgs[3]
                };
            }
        }

        private static void AddLeg(string[] componentArgs, Jarvis jarvis)
        {
            int energyConsumption = int.Parse(componentArgs[1]);
            if (jarvis.Legs.Count > 1)
            {
                if (jarvis.Legs.Any(leg => leg.EnergyConsumption > energyConsumption))
                {
                    jarvis.Legs.Remove(jarvis.Legs.First(leg => leg.EnergyConsumption > energyConsumption));
                    jarvis.Legs.Add(new Leg
                    {
                        EnergyConsumption = int.Parse(componentArgs[1]),
                        Strength = int.Parse(componentArgs[2]),
                        Speed = int.Parse(componentArgs[3])
                    });
                }
            }
            else
            {
                jarvis.Legs.Add(new Leg
                {
                    EnergyConsumption = int.Parse(componentArgs[1]),
                    Strength = int.Parse(componentArgs[2]),
                    Speed = int.Parse(componentArgs[3])
                });
            }
        }

        private static void AddArm(string[] componentArgs, Jarvis jarvis)
        {
            int energyConsumption = int.Parse(componentArgs[1]);
            if (jarvis.Arms.Count > 1)
            {
                if (jarvis.Arms.Any(arm => arm.EnergyConsumption > energyConsumption))
                {
                    jarvis.Arms.Remove(jarvis.Arms.First(arm => arm.EnergyConsumption > energyConsumption));
                    jarvis.Arms.Add(new Arm
                    {
                        EnergyConsumption = int.Parse(componentArgs[1]),
                        ReachDistance = int.Parse(componentArgs[2]),
                        FingersCount = int.Parse(componentArgs[3])
                    });
                }
            }
            else
            {
                jarvis.Arms.Add(new Arm
                {
                    EnergyConsumption = int.Parse(componentArgs[1]),
                    ReachDistance = int.Parse(componentArgs[2]),
                    FingersCount = int.Parse(componentArgs[3])
                });
            }
        }
    }
}
