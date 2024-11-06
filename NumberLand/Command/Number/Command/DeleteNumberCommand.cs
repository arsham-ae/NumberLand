using MediatR;

namespace NumberLand.Command.Number.Command
{
    public class DeleteNumberCommand : IRequest<string>
    {
        public int id { get; set; }
        public DeleteNumberCommand(int Id)
        {
            id = Id;
        }
    }
}
