public  class Knife : Weapon
{
    private const int MIN_DAMAGE = 3;
    private const int MAX_DAMAGE = 4;
    private const int SOCKETS_COUNT = 2;

    public Knife(string name, string rarity)
        : base(MIN_DAMAGE, MAX_DAMAGE, SOCKETS_COUNT, name, rarity)
    {
    }
}
