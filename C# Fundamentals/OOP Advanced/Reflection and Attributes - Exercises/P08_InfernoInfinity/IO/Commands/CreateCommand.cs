public class CreateCommand : Command
{
    private readonly IWeaponFactory _weaponFactory;

    public CreateCommand(string[] data, IWeaponFactory weaponFactory, IRepository repository)
        : base(data, repository)
    {
        _weaponFactory = weaponFactory;
    }

    public override void Execute()
    {
        var args = Data[0].Split();
        var weaponType = args[1];
        var weaponRarity = args[0];
        var weaponName = Data[1];

        var weapon = _weaponFactory.CreateWeapon(weaponType, weaponRarity, weaponName);
        _repository.AddWeapon(weapon);
    }
}
