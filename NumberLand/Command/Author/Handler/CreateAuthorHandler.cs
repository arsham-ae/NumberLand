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

        public CreateAuthorHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<AuthorDTO>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "authors");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.imageFile.CopyToAsync(fileStream);
            }

            var image = Path.Combine("images/authors", uniqueFileName);
            var mappedAuthor = _mapper.Map<AuthorModel>(request.authorDTO);
            mappedAuthor.imagePath = image.Replace("\\", "/");
            mappedAuthor.slug = SlugHelper.GenerateSlug(request.authorDTO.authorName);
            await _unitOfWork.author.Add(mappedAuthor);
            await _unitOfWork.Save();

            return new CommandsResponse<AuthorDTO>
            {
                status = "Success",
                message = "Author Created Successfully.",
                data = _mapper.Map<AuthorDTO>(mappedAuthor)
            };
        }
    }
}
