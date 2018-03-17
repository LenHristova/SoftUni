using System;

public abstract class Tyre
{
    private const double DEFAULT_DEGRADATION = 100;
    private double _degradation;

    protected Tyre(double hardness)
    {
        Hardness = hardness;
        Degradation = DEFAULT_DEGRADATION;
    }

    public string Name => GetType().Name.Replace("Tyre", "");

    public double Hardness { get; }

    protected virtual double MinDegradation => 0;

    public double Degradation
    {
        get => _degradation;
        protected set
        {
            if (value < MinDegradation)
            {
                throw new ArgumentException("Blown Tyre");
            }

            _degradation = value;
        }
    }

    public virtual void Degradate()
    {
        Degradation -= Hardness;
    }
}