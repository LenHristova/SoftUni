public class PrintCommand : Command
{
    private readonly IWriter _writer;

    public PrintCommand(string[] data, IWriter writer, IRepository repository)
        : base(data, repository)
    {
        _writer = writer;
    }

    public override void Execute()
    {
        var weaponName = Data[0];
        var weapon = _repository.GetWeapon(weaponName);

        _writer.WriteLine(weapon.ToString());
    }
}