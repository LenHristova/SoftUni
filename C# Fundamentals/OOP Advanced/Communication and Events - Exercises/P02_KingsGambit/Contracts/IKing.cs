using System.Collections.Generic;

namespace P02_KingsGambit.Contracts
{
    public interface IKing : INameable, IAttackable
    {
        IReadOnlyCollection<ISubordinate> Subordinates { get; }

        void AddSubordinate(ISubordinate subordinate);
    }
}