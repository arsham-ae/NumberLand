using AutoMapper;
using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Blog.Handler
{
    public class UpdateBlogImageHandler : IRequestHandler<UpdateBlogImageCommand, CommandsResponse<BlogDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public UpdateBlogImageHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
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

                blog.featuredImagePath = await _saveImageHelper.SaveImage(request.File, "blogs");
                await _unitOfWork.Save();
                return new CommandsResponse<BlogDTO>
                {
                    status = "Success",
                    message = $"Blog Image With Id {request.Id} Updated Successfully.",
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
