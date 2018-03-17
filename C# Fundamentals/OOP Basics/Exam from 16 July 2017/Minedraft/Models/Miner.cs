public abstract class Miner
{
    private string _id;

    protected Miner(string id)
    {
        Id = id;
    }

    public abstract string Type { get; }
    public string Id
    {
        get { return _id; }
        private set
        {
            Validator.ValidateStringNotNullOrWhiteSpace(value, nameof(Id));
            _id = value;
        }
    }
}