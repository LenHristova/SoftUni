namespace Forum.Models
{
    public class Identifiable : IIdentifiable
    {
        public Identifiable(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}