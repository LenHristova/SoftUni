using System.Collections.Generic;

public class Garage
{
    public Garage()
    {
        ParkedCars = new List<Car>();
    }

    public ICollection<Car> ParkedCars{ get; private set; }

    public void TuneParkedCars(int tuneIndex, string addOn)
    {
        foreach (var car in ParkedCars)
        {
            car.Tune(tuneIndex, addOn);
        }
    }
}