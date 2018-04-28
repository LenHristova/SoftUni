using System.Collections.Generic;

public class SpecialForce : Soldier
{
    private const double OVERALL_SKILL_MILTIPLIER = 3.5;
    private const double REGENERATE_INCREASE = 30;

    protected override IReadOnlyList<string> WeaponsAllowed => new List<string>
        {
            "Gun",
            "AutomaticMachine",
            "MachineGun",
            "RPG",
            "Helmet",
            "Knife",
            "NightVision"
        };

    public SpecialForce(string name, int age, double experience, double endurance) 
        : base( name, age, experience, endurance, OVERALL_SKILL_MILTIPLIER, REGENERATE_INCREASE)
    {
    }
}