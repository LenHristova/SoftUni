using System.Text;

namespace P02_Cars.Models
{
    public class Car : ICar
    {
        public string Model { get; set; }
        public string Color { get; set; }

        public Car(string model, string color)
        {
            Model = model;
            Color = color;
        }

        public string Start()
        {
            return "Engine start";
        }

        public string Stop()
        {
            return "Breaaak!";
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Color} {GetType().Name} {Model}");
            sb.AppendLine(Start());
            sb.Append(Stop());
            return sb.ToString();
        }
    }
}