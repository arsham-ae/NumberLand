using MediatR;
using NumberLand.Command.Number;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Handler.Number
{
    public class DeleteRangeNumberHandler : IRequestHandler<DeleteRangeNumberCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRangeNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteRangeNumberCommand request, CancellationToken cancellationToken)
        {
            var get = _unitOfWork.number.GetAll().Where(p => request.Ids.Contains(p.id)).ToList();
            if (get == null || !get.Any())
            {
                return "Numbers Not Found!";
            }
            _unitOfWork.number.DeleteRange(get);
            _unitOfWork.Save();
            return $"Numbers Deleted Successfully!";
        }
    }
}
