using System.Text;

namespace P02_Cars.Models
{
    public class ElectricCar : Car, ICar, IElectricCar
    {
        public int Battery { get; set; }

        public ElectricCar(string model, string color, int battery) : base(model, color)
        {
            Battery = battery;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Color} {GetType().Name} {Model} with {Battery} Batteries");
            sb.AppendLine(Start());
            sb.Append(Stop());
            return sb.ToString();
        }
    }
}