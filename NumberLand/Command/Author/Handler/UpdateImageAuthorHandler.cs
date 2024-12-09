using AutoMapper;
using MediatR;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Author.Handler
{
    public class UpdateImageAuthorHandler : IRequestHandler<UpdateImageAuthorCommand, CommandsResponse<AuthorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public UpdateImageAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }

        public async Task<CommandsResponse<AuthorDTO>> Handle(UpdateImageAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _unitOfWork.author.Get(a => a.id == request.Id);
                if (author == null)
                {
                    return new CommandsResponse<AuthorDTO>
                    {
                        status = "Fail",
                        message = $"Author With Id {request.Id} Not Found!"
                    };
                }
                author.imagePath = await _saveImageHelper.SaveImage(request.File, "authors");
                await _unitOfWork.Save();
                return new CommandsResponse<AuthorDTO>
                {
                    status = "Success",
                    message = $"Author Image With Id {request.Id} Updated Successfully.",
                    data = _mapper.Map<AuthorDTO>(author)
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
