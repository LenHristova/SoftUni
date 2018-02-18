namespace P01_RawData
{
    public class Car
    {
        public string Model { get; set; }
        public Engine Engine { get; set; }
        public Cargo Cargo { get; set; }
        public TiresSet TiresSet { get; set; }

        public Car(string model, Engine engine, Cargo cargo, TiresSet tiresSet)
        {
            Model = model;
            Engine = engine;
            Cargo = cargo;
            TiresSet = tiresSet;
        }

        public override string ToString()
        {
            return Model;
        }
    }
}