namespace P03_BarraksWars.Models.Units
{
    public class Horseman : Unit
    {
        private const int DEFAULT_HEALTH = 50;
        private const int DEFAULT_DAMAGE = 10;

        public Horseman()
            : base(DEFAULT_HEALTH, DEFAULT_DAMAGE)
        {
        }
    }
}