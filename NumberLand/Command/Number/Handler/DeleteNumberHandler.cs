﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Number.Handler
{
    public class DeleteNumberHandler : IRequestHandler<DeleteNumberCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteNumberHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteNumberCommand request, CancellationToken cancellationToken)
        {
            var number = await _unitOfWork.number.Get(o => o.id == request.id);
            if (number == null)
            {
                return "Id Not Found!";
            }
            _unitOfWork.number.Delete(number);
            await _unitOfWork.Save();
            return $"number {number.number} wiht Id {request.id} Deleted Successfully!";
        }
    }
}