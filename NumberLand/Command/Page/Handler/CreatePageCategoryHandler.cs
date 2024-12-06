using AutoMapper;
using MediatR;
using NumberLand.Command.Page.Command;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Models.Pages;
using NumberLand.Utility;

namespace NumberLand.Command.Page.Handler
{
    public class CreatePageCategoryHandler : IRequestHandler<CreatePageCategoryCommand, CommandsResponse<PageCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePageCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CommandsResponse<PageCategoryDTO>> Handle(CreatePageCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mappedCat = _mapper.Map<PageCategoryModel>(request.PageCategory);
                mappedCat.slug = SlugHelper.GenerateSlug(mappedCat.slug);
                await _unitOfWork.pageCategory.Add(mappedCat);
                await _unitOfWork.Save();
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Success",
                    message = "PageCategory Created Successfully.",
                    data = _mapper.Map<PageCategoryDTO>(await _unitOfWork.pageCategory.Get(p => p.id == mappedCat.id))
                };
            }
            catch (Exception ex)
            {
                return new CommandsResponse<PageCategoryDTO>
                {
                    status = "Fail",
                    message = ex.InnerException.Message
                };
            }
        }
    }
}
