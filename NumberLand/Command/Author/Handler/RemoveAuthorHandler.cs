using AutoMapper;
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
            try
            {
                var author = await _unitOfWork.author.Get(o => o.id == request.Id);
                if (author == null)
                {
                    return new CommandsResponse<AuthorDTO>
                    {
                        status = "Fail",
                        message = $"Author With Id {request.Id} Not Found!"
                    };
                }
                var fullPath = Path.Combine(_environment.WebRootPath, author.imagePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                else
                {
                    throw new FileNotFoundException("File not found.", fullPath);
                }
                _unitOfWork.author.Delete(author);
                await _unitOfWork.Save();
                return new CommandsResponse<AuthorDTO>
                {
                    status = "Success",
                    message = $"Author With Id {request.Id} Deleted Successfully."
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<AuthorDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
