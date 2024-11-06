using MediatR;

namespace NumberLand.Command.Number.Command
{
    public class DeleteRangeNumberCommand : IRequest<string>
    {
        public List<int> Ids { get; set; }
        public DeleteRangeNumberCommand(List<int> ids)
        {
            Ids = ids;
        }
    }
}
