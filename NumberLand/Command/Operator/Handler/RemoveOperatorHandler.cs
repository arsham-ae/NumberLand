﻿using MediatR;
using NumberLand.Command.Operator.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Operator.Handler
{
    public class RemoveOperatorHandler : IRequestHandler<RemoveOperatorCommand, CommandsResponse<OperatorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveOperatorHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<OperatorDTO>> Handle(RemoveOperatorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var @operator = await _unitOfWork.nOperator.Get(o => o.id == request.Id);
                if (@operator == null)
                {
                    return new CommandsResponse<OperatorDTO>
                    {
                        status = "Fail",
                        message = $"Operator with Id {request.Id} Not Found!"
                    };
                }
                _unitOfWork.nOperator.Delete(@operator);
                await _unitOfWork.Save();
                return new CommandsResponse<OperatorDTO>
                {
                    status = "Success",
                    message = $"Operator {@operator.operatorCode} with Id {request.Id} Deleted Successfully!"
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
