using System;
using System.Reflection;

public class WeaponFactory : Factory, IWeaponFactory
{
    public IWeapon CreateWeapon(string weaponType, string weaponRarity, string weaponName)
    {
        var type = Assembly
            .GetExecutingAssembly()
            .GetType(weaponType);

        EnsureType(type, typeof(IWeapon), weaponType);

        var weapon = (IWeapon)Activator.CreateInstance(type, new object[] {weaponName, weaponRarity});

        return weapon;
    }
}