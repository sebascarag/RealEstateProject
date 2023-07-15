using AutoMapper;
using FluentValidation;
using MediatR;
using RealEstate.Application.Contracts;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Properties.Queries
{
    public record GetPropertiesWithFiltersQueryRequest : IRequest<IList<PropertyFilteredDto>>
    {
        public string? Name { get; init; }
        public string? Address { get; init; }
        public decimal MinPrice { get; init; } = 0;
        public decimal? MaxPrice { get; init; }
        public uint MinYear { get; init; }
        public int? MaxYear { get; init; }
        public string? OwnerName { get; init; }
    }

    public class GetPropertiesWithFiltersQueryValidation : AbstractValidator<GetPropertiesWithFiltersQueryRequest>
    {
        public GetPropertiesWithFiltersQueryValidation()
        {
            RuleFor(r => r.MaxPrice)
                .GreaterThanOrEqualTo(r => r.MinPrice)
                .When(r => r.MaxPrice > 0);
            RuleFor(r => r.MaxYear)
                .GreaterThanOrEqualTo(r => (int)r.MinYear)
                .When(r => r.MaxPrice > 0);
        }
    }

    public class GetPropertiesWithFiltersQueryHandler : IRequestHandler<GetPropertiesWithFiltersQueryRequest, IList<PropertyFilteredDto>>
    {
        private readonly IRepository<Property> _propertyRepo;
        private readonly IMapper _mapper;

        public GetPropertiesWithFiltersQueryHandler(IRepository<Property> propertyRepo, IMapper mapper)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
        }
        public async Task<IList<PropertyFilteredDto>> Handle(GetPropertiesWithFiltersQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _propertyRepo.GetAllActiveIncluding(x => x.Owner)
                .WhereIf(p => p.Name.ToLower().Contains((request.Name ?? "").ToLower()), !string.IsNullOrWhiteSpace(request.Name))
                .WhereIf(p => p.Address.ToLower().Contains((request.Address ?? "").ToLower()), !string.IsNullOrWhiteSpace(request.Address))
                .WhereIf(p => p.Owner.Name.ToLower().Contains((request.OwnerName ?? "").ToLower()), !string.IsNullOrWhiteSpace(request.OwnerName))
                .Where(p => p.Year >= request.MinYear)
                .WhereIf(p => p.Year <= request.MaxYear, request.MaxYear > 0)
                .Where(p => p.Price >= request.MinPrice)
                .WhereIf(p => p.Price <= request.MaxPrice, request.MaxPrice > 0);

            var propertyList = await _propertyRepo.ToListAsync(query, cancellationToken);

            return _mapper.Map<IList<PropertyFilteredDto>>(propertyList);
        }
    }
}
