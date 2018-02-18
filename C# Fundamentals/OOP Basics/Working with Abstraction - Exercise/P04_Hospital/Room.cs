using System;
using System.Collections.Generic;

namespace P04_Hospital
{
    public class Room
    {
        public List<Patient> Beds { get; set; } = new List<Patient>();

        public bool HasFreeBed()
        {
            return Beds.Count < 3;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Beds);
        }
    }
}