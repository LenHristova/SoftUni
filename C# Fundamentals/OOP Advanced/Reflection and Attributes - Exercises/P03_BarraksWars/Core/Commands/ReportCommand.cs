using P03_BarraksWars.Attributes;
using P03_BarraksWars.Contracts;

namespace P03_BarraksWars.Core.Commands
{
    public class ReportCommand : Command
    {
        [Inject]
        private readonly IRepository _repository;

        public ReportCommand(string[] data, IRepository repository) 
            : base(data)
        {
            _repository = repository;
        }

        public override string Execute()
        {
            var output = _repository.Statistics;
            return output;
        }
    }
}
