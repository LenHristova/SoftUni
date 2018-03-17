namespace Forum.Models.Contracts
{
    public interface IReply : IIdentifiable
    {
        string Content { get; }
        int AuthorId { get; }
        int PostId { get; }
    }
}