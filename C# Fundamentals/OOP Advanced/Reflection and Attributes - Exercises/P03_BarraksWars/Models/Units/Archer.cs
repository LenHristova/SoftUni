namespace P03_BarraksWars.Models.Units
{
    public class Archer : Unit
    {
        private const int DEFAULT_HEALTH = 25;
        private const int DEFAULT_DAMAGE = 7;

        public Archer() 
            : base(DEFAULT_HEALTH, DEFAULT_DAMAGE)
        {
        }
    }
}
