using AutoMapper;
using MediatR;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Utility;

namespace NumberLand.Command.Blog.Handler
{
    public class CreateBlogCategoryHandler : IRequestHandler<CreateBlogCategoryCommand, CommandsResponse<BlogCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SaveImageHelper _saveImageHelper;
        public CreateBlogCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, SaveImageHelper saveImageHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _saveImageHelper = saveImageHelper;
        }
        public async Task<CommandsResponse<BlogCategoryDTO>> Handle(CreateBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedCat = _mapper.Map<BlogCategoryModel>(request.BlogCatDTO);
                mappedCat.slug = SlugHelper.GenerateSlug2(request.BlogCatDTO.blogCategorySlug);
                mappedCat.icon = await _saveImageHelper.SaveImage(request.File, "blogCategories");
                await _unitOfWork.blogCategory.Add(mappedCat);
                await _unitOfWork.Save();

                return new CommandsResponse<BlogCategoryDTO>
                {
                    status = "Success",
                    message = "BlogCategory Created Successfully.",
                    data = _mapper.Map<BlogCategoryDTO>(mappedCat)
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
