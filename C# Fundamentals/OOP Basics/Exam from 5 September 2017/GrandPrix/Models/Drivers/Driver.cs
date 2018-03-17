public abstract class Driver
{
    private string _status;

    protected Driver(string name, Car car)
    {
        Name = name;
        TotalTime = 0;
        Car = car;
        Status = string.Empty;
    }

    public string Name { get; }

    public double TotalTime { get; private set; }

    public Car Car { get; }

    public abstract double FuelConsumptionPerKm { get; }

    public virtual double Speed => (Car.Hp + Car.Tyre.Degradation) / Car.FuelAmount;

    public string Status
    {
        get => _status == string.Empty ? TotalTime.ToString("F3") : _status;

        set => _status = value;
    }

    public void Drive(int distance)
    {
        TotalTime += 60.0 / (distance / Speed);
        Car.Move(distance, FuelConsumptionPerKm);
    }

    public void AddBoxTime()
    {
        TotalTime += 20;
    }

    public void Overtaking(double seconds)
    {
        TotalTime -= seconds;
    }

    public void BeOvertaked(double seconds)
    {
        TotalTime += seconds;
    }

    public override string ToString()
    {
        return $"{Name} {Status}";
    }
}