using System;
using System.Collections.Generic;
using System.Linq;

[Custom("Pesho", 3, "Used for C# OOP Advanced Course - Enumerations and Attributes.", "Pesho", "Svetlio")]
public abstract class Weapon : IWeapon
{
    private readonly IGem[] _gems;
    private readonly int _minDamage;
    private readonly int _maxDamage;

    protected Weapon(int minDamage, int maxDamage, int socketsCount, string name, string rarity)
    {
        _gems = new IGem[socketsCount];

        Name = name;

        Rarity = SetRarity(rarity);
        _minDamage = minDamage * (int)Rarity;
        _maxDamage = maxDamage * (int)Rarity;
    }

    private Rarity SetRarity(string rarityStr)
    {
        var isValidRarity = Enum.TryParse(typeof(Rarity), rarityStr, out var rarity);
        if (!isValidRarity)
        {
            throw new ArgumentException($"Invalid rarity type: {rarityStr}");
        }

        return (Rarity)rarity;
    }

    public string Name { get; }

    public int MinDamage => StrengthPoints * 2 + AgilityPoints + _minDamage;

    public int MaxDamage => StrengthPoints * 3 + AgilityPoints * 4 + _maxDamage;

    public IReadOnlyCollection<IGem> Gems => _gems;

    public Rarity Rarity { get; }

    public int StrengthPoints => GetGems().Sum(g => g.StrengthIncreasing);

    public int AgilityPoints => GetGems().Sum(g => g.AgilityIncreasing);

    public int VitalityPoints => GetGems().Sum(g => g.VitalityIncreasing);

    private IEnumerable<IGem> GetGems() => _gems.Where(g => g != null);

    public void AddGem(IGem gem, int gemIndex)
    {
        if (IsValidIndex(gemIndex))
        {
            _gems[gemIndex] = gem;
        }
    }

    public void RemoveGem(int gemIndex)
    {
        if (IsValidIndex(gemIndex))
        {
            _gems[gemIndex] = default(IGem);
        }
    }

    private bool IsValidIndex(int gemIndex) => gemIndex >= 0 && gemIndex < _gems.Length;

    public override string ToString()
    {
        return $"{Name}: {MinDamage}-{MaxDamage} Damage, +{StrengthPoints} Strength, +{AgilityPoints} Agility, +{VitalityPoints} Vitality";
    }
}
