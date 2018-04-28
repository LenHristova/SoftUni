using System;
using System.Linq;

namespace StorageMaster.Core
{
    public class Engine
    {
        private StorageMaster storageMaster;

        public Engine()
        {
            storageMaster = new StorageMaster();
        }

        public void Run()
        {

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    var args = input.Split();
                    var output = ParseInput(args).Trim();
                    Console.WriteLine(output);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            Console.WriteLine(storageMaster.GetSummary());
        }

        private string ParseInput(string[] args)
        {

            var command = args[0];

            switch (command)
            {
                case "AddProduct":
                    {
                        var type = args[1];
                        var price = double.Parse(args[2]);
                        return storageMaster.AddProduct(type, price);
                    }
                case "RegisterStorage":
                    {
                        var type = args[1];
                        var name = args[2];
                        return storageMaster.RegisterStorage(type, name);
                    }
                case "SelectVehicle":
                    {
                        var storageName = args[1];
                        var garageSlot = int.Parse(args[2]);
                        return storageMaster.SelectVehicle(storageName, garageSlot);
                    }
                case "LoadVehicle":
                    {
                        var productNames = args.Skip(1);
                        return storageMaster.LoadVehicle(productNames);
                    }
                case "SendVehicleTo":
                    {
                        var sourceName = args[1];
                        var garageSlot = int.Parse(args[2]);
                        var destinationName = args[3];
                        return storageMaster.SendVehicleTo(sourceName, garageSlot, destinationName);
                    }
                case "UnloadVehicle":
                    {
                        var storageName = args[1];
                        var garageSlot = int.Parse(args[2]);
                        return storageMaster.UnloadVehicle(storageName, garageSlot);
                    }
                case "GetStorageStatus":
                    {
                        var storageName = args[1];
                        return storageMaster.GetStorageStatus(storageName);
                    }
                //case "END":
                //    {
                //        return storageMaster.GetSummary();
                //    }

                default:
                    throw new NotSupportedException();
            }

        }
    }
}