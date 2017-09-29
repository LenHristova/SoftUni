using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p01_CenturiesToMinutes
{
    class CenturiesToMinutes
    {
        static void Main(string[] args)
        {
            byte centuries = byte.Parse(Console.ReadLine());
            ushort years = (ushort)(centuries * 100);
            int days = (int)(years * 365.2422);
            uint hours = (uint)(days * 24);
            long minutes = hours * 60;

            Console.WriteLine($"{centuries} centuries = " +
                $"{years} years = {days} days = " +
                $"{hours} hours = {minutes} minutes");
        }
    }
}
