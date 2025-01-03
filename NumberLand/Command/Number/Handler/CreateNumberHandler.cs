﻿using AutoMapper;
using MediatR;
using NumberLand.Command.Number.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Numbers;
using NumberLand.Utility;

namespace NumberLand.Command.Number.Handler
{
    public class CreateNumberHandler : IRequestHandler<CreateNumberCommand, CommandsResponse<NumberDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateNumberHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandsResponse<NumberDTO>> Handle(CreateNumberCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedNum = _mapper.Map<NumberModel>(request.NumberDTO);
                mappedNum.slug = SlugHelper.GenerateSlug2(request.NumberDTO.numberSlug);
                await _unitOfWork.number.Add(mappedNum);
                await _unitOfWork.Save();
                return new CommandsResponse<NumberDTO>
                {
                    status = "Success",
                    message = "Number Created Successfully.",
                    data = _mapper.Map<NumberDTO>(mappedNum)
                };

            }
            catch (Exception ex)
            {
                return new CommandsResponse<NumberDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
