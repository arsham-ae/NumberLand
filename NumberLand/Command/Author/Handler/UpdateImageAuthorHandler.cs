using AutoMapper;
using MediatR;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Author.Handler
{
    public class UpdateImageAuthorHandler : IRequestHandler<UpdateImageAuthorCommand, CommandsResponse<AuthorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public UpdateImageAuthorHandler(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<AuthorDTO>> Handle(UpdateImageAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = _mapper.Map<AuthorDTO>(await _unitOfWork.author.Get(a => a.id == request.Id));
            if (author == null)
            {
                return new CommandsResponse<AuthorDTO>
                {
                    status = "Fail",
                    message = "Author Not Found!",
                };
            }
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "authors");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(fileStream);
            }

            author.imagePath = Path.Combine("images/authors", uniqueFileName).Replace("\\", "/");
            return new CommandsResponse<AuthorDTO>
            {
                status = "Success",
                message = "Image Updated Successfully.",
                data = author
            };

        }
    }
}
