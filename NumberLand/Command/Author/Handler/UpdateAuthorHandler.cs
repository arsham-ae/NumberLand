using AutoMapper;
using MediatR;
using NumberLand.Command.Author.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Author.Handler
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, CommandsResponse<AuthorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public UpdateAuthorHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<CommandsResponse<AuthorDTO>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _unitOfWork.author.Get(p => p.id == request.Id);

                if (author == null)
                {
                    return new CommandsResponse<AuthorDTO>
                    {
                        status = "Fail",
                        message = "Author Not Found!"
                    };
                }
                if (request.File != null && request.File.Length > 0)
                {
                    author.imagePath = await SaveAuthorImage(request.File);
                }
                try
                {
                    request.JsonPatch.ApplyTo(author);

                }
                catch (Exception ex)
                {
                    return new CommandsResponse<AuthorDTO>
                    {
                        status = "Fail",
                        message = $"Error applying patch document: {ex.InnerException.Message}"
                    };
                }
                await _unitOfWork.Save();

                return new CommandsResponse<AuthorDTO>
                {
                    status = "Success",
                    message = "Author Updated Successfully!",
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
        private async Task<string> SaveAuthorImage(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "authors");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine("images/authors", uniqueFileName).Replace("\\", "/");
        }
    }

}
