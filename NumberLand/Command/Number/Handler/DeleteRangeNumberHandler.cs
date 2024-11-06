using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Number.Handler
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
            var get = (await _unitOfWork.number.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
            if (get == null || !get.Any())
            {
                return "Numbers Not Found!";
            }
            _unitOfWork.number.DeleteRange(get);
            await _unitOfWork.Save();
            return $"Numbers Deleted Successfully!";
        }
    }
}
