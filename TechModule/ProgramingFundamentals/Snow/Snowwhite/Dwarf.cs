public class Dwarf
{
    public string Name { get; }

    public string HatColor { get; }

    public int Physics { get; set; }

    public Dwarf(string name, string hatColor, int physics)
    {
        Name = name;
        HatColor = hatColor;
        Physics = physics;
    }

    public override bool Equals(object obj)
    {
        Dwarf dwarf = obj as Dwarf;

        if (dwarf == null)
        {
            return false;
        }
        else
        {
            return (Name + HatColor).Equals(dwarf.Name + dwarf.HatColor);
        }
    }

    public override int GetHashCode()
    {
        return (Name + HatColor).GetHashCode();
    }

    public override string ToString()
    {
        return $"({HatColor}) {Name} <-> {Physics}";
    }
}
