using AutoMapper;
using MediatR;
using NumberLand.Command.Number;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Handler.Number
{
    public class PatchNumberHandler : IRequestHandler<PatchNumberCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatchNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(PatchNumberCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.number.Patch(request.Id, request.PatchDoc);
            return $"Number With Id {request.Id} Updated SuccessFully!";
        }
    }
}
