public class Ruby : Gem
{
    private const int STRENGTH_INCREASING = 7;
    private const int AGILITY_INCREASING = 2;
    private const int VITALITY_INCREASING = 5;

    public Ruby(string clarity) 
        : base(STRENGTH_INCREASING, AGILITY_INCREASING, VITALITY_INCREASING, clarity)
    {
    }
}