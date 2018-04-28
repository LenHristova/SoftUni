using System.Collections.Generic;

public interface IWeapon
{
    IReadOnlyCollection<IGem> Gems { get; }

    int MaxDamage { get; }

    int MinDamage { get; }

    string Name { get; }

    Rarity Rarity { get; }

    int StrengthPoints { get; }

    int AgilityPoints { get; }

    int VitalityPoints { get; }

    void AddGem(IGem gem, int gemIndex);

    void RemoveGem(int gemIndex);
}