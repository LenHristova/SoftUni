namespace P04_Hospital
{
    public class Patient
    {
        public string Name { get; set; }

        public Patient(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}