using System;
using System.Text;

namespace P02_CarsSalesman
{
    public class Car
    {
        private const string offset = "  ";

        public string model;
        public Engine engine;
        public int weight;
        public string color;

        public Car(string model, Engine engine)
        {
            this.model = model;
            this.engine = engine;
            this.weight = -1;
            this.color = "n/a";
        }

        public Car(string model, Engine engine, int weight):this(model, engine)
        {
            this.weight = weight;
        }

        public Car(string model, Engine engine, string color):this(model, engine)
        {
            this.color = color;
        }

        public Car(string model, Engine engine, int weight, string color):this(model, engine)
        {
            this.weight = weight;
            this.color = color;
        }


        public override string ToString()
        {
            string weightToString = this.weight == -1 ? "n/a" : this.weight.ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat($"{this.model}:{Environment.NewLine}");
            sb.Append(this.engine);
            sb.AppendFormat($"{offset}Weight: {weightToString}{Environment.NewLine}");
            sb.AppendFormat($"{offset}Color: {this.color}");

            return sb.ToString();
        }
    }
}