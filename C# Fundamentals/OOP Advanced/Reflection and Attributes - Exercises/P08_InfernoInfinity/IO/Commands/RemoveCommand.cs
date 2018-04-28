public class RemoveCommand : Command
{
    public RemoveCommand(string[] data, IRepository repository) 
        : base(data, repository)
    {
    }

    public override void Execute()
    {
        var weaponName = Data[0];
        var gemIndex = int.Parse(Data[1]);
        _repository.RemoveWeaponGem(weaponName, gemIndex);
    }
}