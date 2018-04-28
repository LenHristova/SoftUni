using System.Collections.Generic;

public class Ranker:Soldier
{
    private const double OVERALL_SKILL_MILTIPLIER = 1.5;
    private const double REGENERATE_INCREASE = 10;

    protected override IReadOnlyList<string> WeaponsAllowed => new List<string>
    {
        "Gun",
        "AutomaticMachine",
        "Helmet",
    };

    public Ranker(string name, int age, double experience, double endurance)
        : base(name, age, experience, endurance, OVERALL_SKILL_MILTIPLIER, REGENERATE_INCREASE)
    {
    }
}