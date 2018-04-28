﻿public class Axe : Weapon
{
    private const int MIN_DAMAGE = 5;
    private const int MAX_DAMAGE = 10;
    private const int SOCKETS_COUNT = 4;

    public Axe(string name, string rarity) 
        : base(MIN_DAMAGE, MAX_DAMAGE, SOCKETS_COUNT, name, rarity)
    {
    }
}
