using AutoMapper;
using MediatR;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Utility;

namespace NumberLand.Command.Author.Handler
{
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, CommandsResponse<AuthorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public CreateAuthorHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }

        public async Task<CommandsResponse<AuthorDTO>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedAuthor = _mapper.Map<AuthorModel>(request.authorDTO);
                mappedAuthor.imagePath = await _saveImageHelper.SaveImage(request.imageFile, "authors");
                mappedAuthor.slug = SlugHelper.GenerateSlug(request.authorDTO.authorSlug);
                await _unitOfWork.author.Add(mappedAuthor);
                await _unitOfWork.Save();

                return new CommandsResponse<AuthorDTO>
                {
                    status = "Success",
                    message = "Author Created Successfully.",
                    data = _mapper.Map<AuthorDTO>(mappedAuthor)
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
