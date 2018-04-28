using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Soldier : ISoldier
{
    private const int MAX_ENDURANCE = 100;

    private double _overallSkillMiltiplier;
    private double _regenerateIncrease;
    private double _endurance;

    protected Soldier(string name, int age, double experience, double endurance, double overallSkillMiltiplier, double regenerateIncrease)
    {
        _overallSkillMiltiplier = overallSkillMiltiplier;
        _regenerateIncrease = regenerateIncrease;
        _endurance = endurance;
        Name = name;
        Age = age;
        Experience = experience;
        SetWeapons();
    }

    private void SetWeapons()
    {
        Weapons = WeaponsAllowed
            .ToDictionary(weaponName => weaponName, weaponName => (IAmmunition)null);
    }

    public string Name { get; }

    public int Age { get; }

    public double Experience { get; private set; }

    public double Endurance
    {
        get => _endurance;
        private set => _endurance = Math.Min(value, MAX_ENDURANCE);
    }

    public double OverallSkill => (Age + Experience) * _overallSkillMiltiplier;

    protected abstract IReadOnlyList<string> WeaponsAllowed { get; }

    public IDictionary<string, IAmmunition> Weapons { get; private set; }

    public void Regenerate()
    {
        Endurance += Age + _regenerateIncrease;
    }

    public bool ReadyForMission(IMission mission)
    {
        if (this.Endurance < mission.EnduranceRequired)
        {
            return false;
        }

        bool hasAllEquipment = this.Weapons.Values.Count(weapon => weapon == null) == 0;

        if (!hasAllEquipment)
        {
            return false;
        }

        return this.Weapons.Values.Count(weapon => weapon.WearLevel <= 0) == 0;
    }

    public void CompleteMission(IMission mission)
    {
        Endurance -= mission.EnduranceRequired;
        Experience += mission.EnduranceRequired;

        AmmunitionRevision(mission.WearLevelDecrement);
    }

    private void AmmunitionRevision(double missionWearLevelDecrement)
    {
        IEnumerable<string> keys = this.Weapons.Keys.ToList();
        foreach (string weaponName in keys)
        {
            this.Weapons[weaponName].DecreaseWearLevel(missionWearLevelDecrement);

            if (this.Weapons[weaponName].WearLevel <= 0)
            {
                this.Weapons[weaponName] = null;
            }
        }
    }

    public override string ToString() => string.Format(OutputMessages.SOLDIER_TO_STRING, this.Name, this.OverallSkill);
}