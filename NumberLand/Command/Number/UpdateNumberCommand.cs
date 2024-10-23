using MediatR;

namespace NumberLand.Command.Number
{
    public class UpdateNumberCommand : IRequest<UpdateNumberResponse>
    {
        public int id { get; set; }
        public string number { get; set; }
        public string application { get; set; }
        public int categoryId { get; set; }
        public int operatorId { get; set; }
        public DateTime expireTime { get; set; }
        public decimal price { get; set; }
    }

    public class UpdateNumberResponse
    {
        public int Id { get; set; }
        public string message { get; set; }
    }
}
