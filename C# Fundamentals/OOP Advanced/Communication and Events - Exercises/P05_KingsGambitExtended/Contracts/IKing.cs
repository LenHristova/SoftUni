using System.Collections.Generic;

namespace P05_KingsGambitExtended.Contracts
{
    public interface IKing : INameable, IAttackable
    {
        IReadOnlyCollection<ISubordinate> Subordinates { get; }

        void AddSubordinate(ISubordinate subordinate);

        void OnSubordinateDeath(object sender);
    }
}