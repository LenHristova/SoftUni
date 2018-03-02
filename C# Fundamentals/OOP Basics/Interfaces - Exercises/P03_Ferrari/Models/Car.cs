namespace P03_Ferrari.Models
{
    public class Car : ICar
    {
        public string Model { get; }
        public string DriverName { get; }

        public Car(string model, string driverName)
        {
            Model = model;
            DriverName = driverName;
        }

        public string UseBrakes()
        {
            return "Brakes!";
        }

        public string PushTheGasPedal()
        {
            return "Zadu6avam sA!";
        }

        public override string ToString()
        {
            return $"{Model}/{UseBrakes()}/{PushTheGasPedal()}/{DriverName}";
        }
    }
}