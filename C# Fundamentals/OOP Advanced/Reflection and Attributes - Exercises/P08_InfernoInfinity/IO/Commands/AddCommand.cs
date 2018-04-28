public class AddCommand : Command
{
    private readonly IGemFactory _gemFactory;

    public AddCommand(string[] data, IGemFactory gemFactory, IRepository repository) : base(data, repository)
    {
        _gemFactory = gemFactory;
    }

    public override void Execute()
    {
        var weaponName = Data[0];
        var gemIndex = int.Parse(Data[1]);

        var gemArgs = Data[2].Split();
        var gemType = gemArgs[1];
        var gemClarity = gemArgs[0];
        var gem = _gemFactory.CreateGem(gemType, gemClarity);

        _repository.AddGemToWeapon(weaponName, gem, gemIndex);
    }
}