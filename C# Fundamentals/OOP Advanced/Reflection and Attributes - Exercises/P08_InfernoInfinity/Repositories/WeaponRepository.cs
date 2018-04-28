using System;
using System.Collections.Generic;

public class WeaponRepository : IRepository
{
    private readonly Dictionary<string, IWeapon> _weapons;

    public WeaponRepository()
    {
        _weapons = new Dictionary<string, IWeapon>();
    }

    public void AddWeapon(IWeapon weapon)
    {
        if (!_weapons.ContainsKey(weapon.Name))
        {
            _weapons.Add(weapon.Name, weapon);
        }
        else
        {
            throw new ArgumentException($"Repository contains weapon with name \"{weapon.Name}\"!");
        }
    }

    public void AddGemToWeapon(string weaponName, IGem gem, int gemIndex)
    {
        EnsureWeapon(weaponName);

        var weapon = _weapons[weaponName];
        weapon.AddGem(gem, gemIndex);
    }

    private void EnsureWeapon(string weaponName)
    {
        if (!_weapons.ContainsKey(weaponName))
        {
            throw new ArgumentException($"There is no weapon with name \"{weaponName}\" in repository!");
        }
    }

    public void RemoveWeaponGem(string weaponName, int gemIndex)
    {
        EnsureWeapon(weaponName);

        var weapon = _weapons[weaponName];
        weapon.RemoveGem(gemIndex);
    }

    public IWeapon GetWeapon(string weaponName)
    {
        EnsureWeapon(weaponName);
        return _weapons[weaponName];
    }
}