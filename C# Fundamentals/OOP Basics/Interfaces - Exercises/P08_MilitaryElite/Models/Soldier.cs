public abstract class Soldier : ISoldier
{
    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }

    protected Soldier(string id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
    {
        return $"Name: {FirstName} {LastName} Id: {Id}";
    }
}