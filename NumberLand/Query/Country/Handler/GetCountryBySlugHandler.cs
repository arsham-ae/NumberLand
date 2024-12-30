using AutoMapper;
using MediatR;
using NumberLand.DataAccess.DTOs;
using NumberLand.DataAccess.Repository.IRepository;
using NumberLand.Query.Country.Query;

namespace NumberLand.Query.Country.Handler
{
    public class GetCountryBySlugHandler : IRequestHandler<GetCountryBySlugQuery, CountryDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCountryBySlugHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CountryDTO> Handle(GetCountryBySlugQuery request, CancellationToken cancellationToken)
        {
            var country = _mapper.Map<CountryDTO>(await _unitOfWork.country.Get(c => c.slug == request.Slug));
            return country == null ? null : country;
        }
    }
}
