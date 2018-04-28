public interface IWeaponFactory
{
    IWeapon CreateWeapon(string weaponType, string weaponRarity, string weaponName);
}