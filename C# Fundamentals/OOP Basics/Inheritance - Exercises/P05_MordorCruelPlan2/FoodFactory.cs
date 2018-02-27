using P05_MordorCruelPlan2.ModelsFood;

namespace P05_MordorCruelPlan2
{
    public static class FoodFactory
    {
        public static Food ProduceFood(string food)
        {
            switch (food.ToLower())
            {
                case "cram":
                    return new Cram();
                case "lembas":
                    return new Lembas();
                case "apple":
                    return new Apple();
                case "melon":
                    return new Melon();
                case "honeycake":
                    return new HoneyCake();
                case "mushrooms":
                    return new Mushrooms();
                default:
                    return new Other();

            }
        }
    }
}