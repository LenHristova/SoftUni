namespace P05_MordorCruelPlan2.ModelsFood
{
    public class Mushrooms : Food
    {
        private const int HAPPINESS_POINTS = -10;

        public Mushrooms() : base(HAPPINESS_POINTS)
        {
        }
    }
}