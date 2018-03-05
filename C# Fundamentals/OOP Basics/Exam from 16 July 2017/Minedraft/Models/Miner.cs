public abstract class Miner
{
    private string _id;

    protected Miner(string id)
    {
        Id = id;
    }

    public string Id
    {
        get { return _id; }
        protected set
        {
            Validator.ValidateStringNotNullOrWhiteSpace(value, nameof(Id));
            _id = value;
        }
    }
}