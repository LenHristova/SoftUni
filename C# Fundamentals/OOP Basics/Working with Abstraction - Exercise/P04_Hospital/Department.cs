using System;
using System.Collections.Generic;
using System.Linq;

namespace P04_Hospital
{
    public class Department
    {
        public string Name { get; set; }
        public List<Room> Rooms { get; } = new List<Room>();

        public bool HasFreeBed()
        {
            return Rooms.Last().HasFreeBed() || Rooms.Count < 20;
        }

        public Department(string name)
        {
            Name = name;
            Rooms.Add(new Room());
        }

        public void PlacePatient(Patient pacient)
        {
            if (!Rooms.Last().HasFreeBed())
            {
                Rooms.Add(new Room());          
            }

            Rooms.Last().Beds.Add(pacient);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Rooms);
        }
    }
}