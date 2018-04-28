public  class Amethyst : Gem
{
    private const int STRENGTH_INCREASING = 2;
    private const int AGILITY_INCREASING = 8;
    private const int VITALITY_INCREASING = 4;

    public Amethyst(string clarity)
        : base(STRENGTH_INCREASING, AGILITY_INCREASING, VITALITY_INCREASING, clarity)
    {
    }
}