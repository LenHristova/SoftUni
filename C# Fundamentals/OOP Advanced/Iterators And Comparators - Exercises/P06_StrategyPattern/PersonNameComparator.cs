using System.Collections.Generic;
using System.Linq;

namespace P06_StrategyPattern
{
    public class PersonNameComparator : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            var lengthComparisson = x.Name.Length.CompareTo(y.Name.Length);

            if (lengthComparisson == 0)
            {
                return x.Name.ToLower().First().CompareTo(y.Name.ToLower().First());
            }

            return lengthComparisson;
        }
    }
}