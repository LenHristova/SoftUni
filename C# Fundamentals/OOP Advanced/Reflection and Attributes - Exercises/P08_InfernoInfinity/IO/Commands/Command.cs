public abstract class Command : IExecutable
{
    protected readonly IRepository _repository;

    protected Command(string[] data, IRepository repository)
    {
        Data = data;
        _repository = repository;
    }

    public string[] Data { get; }

    public abstract void Execute();
}