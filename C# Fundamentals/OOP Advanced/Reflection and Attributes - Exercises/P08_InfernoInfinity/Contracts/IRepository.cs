public interface IRepository
{
    void AddWeapon(IWeapon weapon);

    void AddGemToWeapon(string weaponName, IGem gem, int gemIndex);

    void RemoveWeaponGem(string weaponName, int gemIndex);

    IWeapon GetWeapon(string weaponName);
}