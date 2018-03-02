namespace P05_BorderControl.Models
{
    public class Citizen : IIdentifiable
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Citizen(string id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }
    }
}