using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Page.Query;

namespace NumberLand.Query.Page.Handler
{
    public class GetAllPagesHandler : IRequestHandler<GetAllPagesQuery, IEnumerable<PageDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllPagesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PageDTO>> Handle(GetAllPagesQuery request, CancellationToken cancellationToken)
        {
            var getall = _mapper.Map<List<PageDTO>>(await _unitOfWork.page.GetAll(includeProp: "category"));
            return getall == null ? null : getall;
        }
    }
}
