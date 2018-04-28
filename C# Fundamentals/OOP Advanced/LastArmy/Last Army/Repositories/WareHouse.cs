using System;
using System.Collections.Generic;
using System.Linq;

public class WareHouse : IWareHouse
{
    private Dictionary<string, List<IAmmunition>> _ammunitions;

    public WareHouse()
    {
        this._ammunitions = new Dictionary<string, List<IAmmunition>>();
    }

    public void AddAmmunition(IAmmunition ammunition)
    {
        if (!_ammunitions.ContainsKey(ammunition.Name))
        {
            _ammunitions[ammunition.Name] = new List<IAmmunition>();
        }

        _ammunitions[ammunition.Name].Add(ammunition);
    }

    public void EquipArmy(IArmy army)
    {
        foreach (var soldier in army.Soldiers)
        {
            TryEquipSoldier(soldier);
        }
    }

    public bool TryEquipSoldier(ISoldier soldier)
    {
        var wornOutWeapons = soldier.Weapons
            .Where(w => w.Value == null)
            .Select(w => w.Key)
            .ToList();

        var isSoldierEquiped = true;
        foreach (var weapon in wornOutWeapons)
        {
            if (_ammunitions.ContainsKey(weapon)
                && _ammunitions[weapon].Count > 0)
            {
                var neededWeapon = _ammunitions[weapon].Last();
                soldier.Weapons[weapon] = neededWeapon;
                _ammunitions[weapon].RemoveAt(_ammunitions[weapon].Count - 1);
            }
            else
            {
                isSoldierEquiped = false;
            }
        }
        return isSoldierEquiped;
    }


}