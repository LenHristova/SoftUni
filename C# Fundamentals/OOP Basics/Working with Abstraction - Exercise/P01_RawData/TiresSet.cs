using System.Collections.Generic;
using System.Linq;

namespace P01_RawData
{
    public class TiresSet
    {
        public List<Tire> Tires { get; set; } = new List<Tire>();

        /// <summary>
        /// Count of tires specifications must be 8;
        /// Every car has 4 tires. Every tire has pressure and age.
        /// </summary>
        /// <param name="tiresSpecifics"></param>
        public TiresSet(string[] tiresSpecifics)
        {
            for (int tire = 0; tire < 4; tire++)
            {
                double tirePressure = double.Parse(tiresSpecifics[tire * 2]);
                int tireAge = int.Parse(tiresSpecifics[tire * 2 + 1]);
                Tires.Add(new Tire(tirePressure, tireAge));
            }
        }

        public bool HasTiresWithNotEnoughtPressure()
        {
            return Tires.Any(t => t.Pressure < 1);
        }
    }
}