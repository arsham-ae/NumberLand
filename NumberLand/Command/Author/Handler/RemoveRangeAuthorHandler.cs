using AutoMapper;
using MediatR;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Author.Handler
{
    public class RemoveRangeAuthorHandler : IRequestHandler<RemoveRangeAuthorCommand, CommandsResponse<AuthorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public RemoveRangeAuthorHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
        }
        public async Task<CommandsResponse<AuthorDTO>> Handle(RemoveRangeAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var get = (await _unitOfWork.author.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (!get.Any() || get == null)
                {
                    return new CommandsResponse<AuthorDTO>
                    {
                        status = "Fail",
                        message = "Authors Not Found!",
                    };
                }
                _unitOfWork.author.DeleteRange(get);
                await _unitOfWork.Save();

                return new CommandsResponse<AuthorDTO>
                {
                    status = "Success",
                    message = "Authors Deleted Successfully.",
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<AuthorDTO>
                {
                    status = "Fail",
                    message = ex.Message
                };
            }
        }
    }
}
