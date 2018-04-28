namespace P03_BarraksWars.Models.Units
{
    public class Swordsman : Unit
    {
        private const int DEFAULT_HEALTH = 40;
        private const int DEFAULT_DAMAGE = 13;

        public Swordsman()
            : base(DEFAULT_HEALTH, DEFAULT_DAMAGE)
        {

        }
    }
}
