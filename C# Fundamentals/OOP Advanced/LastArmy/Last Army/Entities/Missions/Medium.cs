public class Medium: Mission
{
    private const string NAME = "Capturing dangerous criminals";
    private const double ENDURANCE_REQUIRED = 50;
    private const double WEAR_LEVEL_DECREMENT = 30;

    public Medium(double scoreToComplete) 
        : base(NAME, ENDURANCE_REQUIRED, scoreToComplete, WEAR_LEVEL_DECREMENT)
    {
    }
}