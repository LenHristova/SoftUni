namespace P03_BarraksWars.Models.Units
{
    public class Pikeman : Unit
    {
        private const int DEFAULT_HEALTH = 30;
        private const int DEFAULT_DAMAGE = 15;

        public Pikeman() 
            : base(DEFAULT_HEALTH, DEFAULT_DAMAGE)
        {
        }
    }
}
