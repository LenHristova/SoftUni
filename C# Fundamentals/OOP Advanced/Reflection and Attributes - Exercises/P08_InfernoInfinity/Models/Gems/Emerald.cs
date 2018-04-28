public  class Emerald : Gem
{
    private const int STRENGTH_INCREASING = 1;
    private const int AGILITY_INCREASING = 4;
    private const int VITALITY_INCREASING = 9;

    public Emerald(string clarity)
        : base(STRENGTH_INCREASING, AGILITY_INCREASING, VITALITY_INCREASING, clarity)
    {
    }
}