using P05_MordorCruelPlan2.ModelsMood;

namespace P05_MordorCruelPlan2
{
    public static class MoodFactory
    {
        public static Mood CreateMood(int happinessPoints)
        {
            if (happinessPoints < -5)
            {
                return new Angry(happinessPoints);
            }

            if (happinessPoints < 1)
            {
                return new Sad(happinessPoints);
            }

            if (happinessPoints < 16)
            {
                return new Happy(happinessPoints);
            }

            return new JavaScript(happinessPoints);
        }
    }
}