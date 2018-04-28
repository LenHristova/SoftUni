using P03_BarraksWars.Attributes;
using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Core.Commands
{
    public class AddCommand : Command
    {
        [Inject]
        private readonly IRepository _repository;
        [Inject]
        private readonly IUnitFactory _unitFactory;

        public AddCommand(string[] data, IRepository repository, IUnitFactory unitFactory)
: base(data)
        {
            _unitFactory = unitFactory;
            _repository = repository;
        }

        public override string Execute()
        {
            var unitType = Data[1];
            var unitToAdd = _unitFactory.CreateUnit(unitType);
            _repository.AddUnit(unitToAdd);
            var output = unitType + " added!";
            return output;
        }
    }
}
