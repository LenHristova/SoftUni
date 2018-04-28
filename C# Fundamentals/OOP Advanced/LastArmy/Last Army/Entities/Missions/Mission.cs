public abstract class Mission : IMission
{
    protected Mission(string name, double enduranceRequired, double scoreToComplete, double wearLevelDecrement)
    {
        Name = name;
        EnduranceRequired = enduranceRequired;
        ScoreToComplete = scoreToComplete;
        WearLevelDecrement = wearLevelDecrement;
    }

    public string Name { get; }

    public double EnduranceRequired { get; }

    public double ScoreToComplete { get; }

    public double WearLevelDecrement { get; }
}