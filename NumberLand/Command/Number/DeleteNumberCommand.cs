using MediatR;

namespace NumberLand.Command.Number
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
