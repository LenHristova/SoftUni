using System;

public class Car
{
    private const double MAX_TANK_CAPACITY = 160;

    private double _fuelAmount;

    public Car(int hp, double fuelAmount, Tyre tyre)
    {
        Hp = hp;
        FuelAmount = fuelAmount;
        Tyre = tyre;
    }

    public int Hp { get; }

    public double FuelAmount
    {
        get => _fuelAmount;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Out of fuel");
            }

            _fuelAmount = Math.Min(value, MAX_TANK_CAPACITY);
        }
    }

    public Tyre Tyre { get; private set; }

    public void ChangeTyres(Tyre tyre)
    {
        Tyre = tyre;
    }

    public void Refuel(double fuelAmount)
    {
        FuelAmount += fuelAmount;
    }

    public void Move(int distance, double fuelConsumptionPerKm)
    {
        FuelAmount -= distance * fuelConsumptionPerKm;
        Tyre.Degradate();
    }
}