namespace P03_Ferrari.Models
{
    public interface ICar
    {
        string Model { get; }
        string DriverName { get; }

        string UseBrakes();
        string PushTheGasPedal();
    }
}