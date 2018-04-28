using System.Collections.Generic;

namespace DungeonsAndCodeWizards.Contracts
{
    public interface IBag
    {
        int Capacity { get; }
        IReadOnlyCollection<IItem> Items { get; }
        int Load { get; }

        void AddItem(IItem item);
        IItem GetItem(string name);
    }
}