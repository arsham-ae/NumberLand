using AutoMapper;
using MediatR;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;

namespace NumberLand.Command.Operator.Handler
{
    public class CreateOperatorHandler : IRequestHandler<CreateOperatorCommand, CommandsResponse<OperatorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOperatorHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<OperatorDTO>> Handle(CreateOperatorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedOperator = _mapper.Map<OperatorModel>(request.OperatorModel);
                mappedOperator.slug = SlugHelper.GenerateSlug2(request.OperatorModel.slug);
                await _unitOfWork.nOperator.Add(mappedOperator);
                await _unitOfWork.Save();
                return new CommandsResponse<OperatorDTO>
                {
                    status = "Success",
                    message = "Operator Created Successfully.",
                    data = _mapper.Map<OperatorDTO>(mappedOperator)
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<OperatorDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
