using AutoMapper;
using Markdig;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NumberLand.Command.Blog.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Blogs;
using NumberLand.Utility;
using System.Reflection.Metadata;

namespace NumberLand.Command.Blog.Handler
{
    public class UpdateBlogCategoryHandler : IRequestHandler<UpdateBlogCategoryCommand, CommandsResponse<BlogCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBlogCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CommandsResponse<BlogCategoryDTO>> Handle(UpdateBlogCategoryCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.blogCategory.Patch(request.Id, request.PatchDoc);

            return new CommandsResponse<BlogCategoryDTO>
            {
                status = "Success",
                message = "BlogCategory Updated Successfully.",
                data = _mapper.Map<BlogCategoryDTO>(await _unitOfWork.blogCategory.Get(bc => bc.id == request.Id))
            };
        }
    }
}
