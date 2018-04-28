using P03_BarraksWars.Attributes;
using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Core.Commands
{
    public class RetireCommand : Command
    {
        [Inject]
        private readonly IRepository _repository;

        public RetireCommand(string[] data, IRepository repository)
            : base(data)
        {
            _repository = repository;
        }

        public override string Execute()
        {
            var unitType = Data[1];
            _repository.RemoveUnit(unitType);
            return unitType + " retired!";
        }
    }
}
