using System.Collections.Generic;

namespace HornetArmada
{
    class Legion
    {
        public long LastActivity { get; set; }
        public Dictionary<string, long> SoldersTypeAndCount { get; set; }
    }
}