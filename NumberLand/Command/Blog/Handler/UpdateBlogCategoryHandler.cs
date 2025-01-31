using AutoMapper;
using Markdig;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Utility;

namespace NumberLand.Command.Blog.Handler
{
    public class UpdateBlogCategoryHandler : IRequestHandler<UpdateBlogCategoryCommand, CommandsResponse<BlogCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public UpdateBlogCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }
        public async Task<CommandsResponse<BlogCategoryDTO>> Handle(UpdateBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var blogCategory = await _unitOfWork.blogCategory.Get(p => p.id == request.Id);
                if (blogCategory == null)
                {
                    return new CommandsResponse<BlogCategoryDTO>
                    {
                        status = "Fail",
                        message = $"BlogCategory With Id {request.Id} Not Found!"
                    };
                }
                var blogSlug = blogCategory.slug;
                if (request.File != null && request.File.Length > 0)
                {
                    blogCategory.icon = await _saveImageHelper.SaveImage(request.File, "blogCategories");
                }
                if (request.PatchDoc != null)
                {
                    request.PatchDoc.ApplyTo(blogCategory);
                    if (blogCategory.slug != blogSlug)
                    {
                        blogCategory.slug = SlugHelper.GenerateSlug(blogCategory.slug);
                    }
                }
                await _unitOfWork.Save();
                return new CommandsResponse<BlogCategoryDTO>
                {
                    status = "Success",
                    message = $"BlogCategory With Id {request.Id} Updated SuccessFully!",
                    data = _mapper.Map<BlogCategoryDTO>(await _unitOfWork.blogCategory.Get(b => b.id == blogCategory.id))
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<BlogCategoryDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
