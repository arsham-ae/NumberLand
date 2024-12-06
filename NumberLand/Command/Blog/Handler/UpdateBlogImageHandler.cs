using AutoMapper;
using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;

namespace NumberLand.Command.Blog.Handler
{
    public class UpdateBlogImageHandler : IRequestHandler<UpdateBlogImageCommand, CommandsResponse<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public UpdateBlogImageHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<CommandsResponse<BlogDTO>> Handle(UpdateBlogImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blog = await _unitOfWork.blog.Get(b => b.id == request.Id);
                if (blog == null)
                {
                    return new CommandsResponse<BlogDTO>
                    {
                        status = "Fail",
                        message = $"Blog With Id {request.Id} Not Found!"
                    };
                }
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "images", "blogs");
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
                blog.featuredImagePath = Path.Combine("images/blogs", uniqueFileName).Replace("\\", "/");
                await _unitOfWork.Save();
                return new CommandsResponse<BlogDTO>
                {
                    status = "Success",
                    message = "image Updated Successfully.",
                    data = _mapper.Map<BlogDTO>(blog)
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<BlogDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
