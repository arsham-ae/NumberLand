using AutoMapper;
using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Number.Handler
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
            await _unitOfWork.number.PatchAsync(request.Id, request.PatchDoc);
            return $"Number With Id {request.Id} Updated SuccessFully!";
        }
    }
}
