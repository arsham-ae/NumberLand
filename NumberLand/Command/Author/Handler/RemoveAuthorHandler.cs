﻿using AutoMapper;
using MediatR;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Author.Handler
{
    public class RemoveAuthorHandler : IRequestHandler<RemoveAuthorCommand, CommandsResponse<AuthorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public RemoveAuthorHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
        }
        public async Task<CommandsResponse<AuthorDTO>> Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
        {
            var get = await _unitOfWork.author.Get(o => o.id == request.Id);
            if (get == null)
            {
                return new CommandsResponse<AuthorDTO>
                {
                    status = "Fail",
                    message = "Author Not Found!"
                };
            }
            _unitOfWork.author.Delete(get);
            await _unitOfWork.Save();
            return new CommandsResponse<AuthorDTO>
            {
                status = "Success",
                message = "Author Deleted Successfully!"
            };
        }
    }
}