public class UltrasoftTyre : Tyre
{
    public UltrasoftTyre(double hardness, double grip) 
        : base(hardness)
    {
        Grip = grip;
    }

    public double Grip { get; private set; }

    protected override double MinDegradation => 30;

    public override void Degradate()
    {
        Degradation -= (Hardness + Grip);
    }
}