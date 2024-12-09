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
                var authors = (await _unitOfWork.author.GetAll()).Where(p => request.Ids.Contains(p.id)).ToList();
                if (!authors.Any() || authors == null)
                {
                    return new CommandsResponse<AuthorDTO>
                    {
                        status = "Fail",
                        message = $"Authors With Id {string.Join(",", request.Ids)} Not Found!"
                    };
                }
                foreach (var author in authors)
                {
                    var fullPath = Path.Combine(_environment.WebRootPath, author.imagePath);
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                    else
                    {
                        throw new FileNotFoundException("File not found.", fullPath);
                    }
                }
                _unitOfWork.author.DeleteRange(authors);
                await _unitOfWork.Save();

                return new CommandsResponse<AuthorDTO>
                {
                    status = "Success",
                    message = $"Authors With Id {string.Join(",", request.Ids)} Deleted Successfully."
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
