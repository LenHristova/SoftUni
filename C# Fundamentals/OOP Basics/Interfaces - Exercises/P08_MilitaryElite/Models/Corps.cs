using System;

public class Corps
{
    private string _name;

    public string Name
    {
        get => _name;
        private set
        {
            if (value != "Airforces" && value != "Marines")
            {
                throw new ArgumentException("Invalid corps!");
            }

            _name = value;
        }
    }

    public Corps(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}