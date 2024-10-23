using MediatR;

namespace NumberLand.Command
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
