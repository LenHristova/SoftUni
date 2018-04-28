using System;
using System.Collections.Generic;
using System.Text;

using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Data
{
    public class UnitRepository : IRepository
    {
        private readonly IDictionary<string, int> _amountOfUnits;

        public UnitRepository()
        {
            _amountOfUnits = new SortedDictionary<string, int>();
        }

        public string Statistics
        {
            get
            {
                var statBuilder = new StringBuilder();
                foreach (var entry in _amountOfUnits)
                {
                    var formatedEntry =
                            string.Format("{0} -> {1}", entry.Key, entry.Value);
                    statBuilder.AppendLine(formatedEntry);
                }

                return statBuilder.ToString().Trim();
            }
        }

        public void AddUnit(IUnit unit)
        {
            var unitType = unit.GetType().Name;
            if (!_amountOfUnits.ContainsKey(unitType))
            {
                _amountOfUnits.Add(unitType, 0);
            }

            _amountOfUnits[unitType]++;
        }

        public void RemoveUnit(string unitType)
        {
            if (!_amountOfUnits.ContainsKey(unitType) || _amountOfUnits[unitType] <= 0)
            {
                throw new InvalidOperationException("No such units in repository.");
            }

            _amountOfUnits[unitType]--;
        }
    }
}
