using MediatR;
using NumberLand.DataAccess.DTOs;

namespace NumberLand.Command.Number.Command
{
    public class CreateNumberCommand : IRequest<string>
    {
        public string number { get; set; }
        public string application { get; set; }
        public int categoryId { get; set; }
        public int operatorId { get; set; }
        public DateTime expireTime { get; set; }
        public decimal price { get; set; }
    }
}
