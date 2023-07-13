using FluentValidation;
using MediatR;
using RealEstate.Application.Contracts;
using RealEstate.Application.Wrappers;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Properties.Command
{
    public record UpdatePropertyPriceCommandRequest : IRequest<bool>
    {
        public int PropertyId { get; init; }
        public decimal Price { get; init; }
    }

    public class UpdatePropertyPriceCommandValidation : AbstractValidator<UpdatePropertyPriceCommandRequest>
    {
        public UpdatePropertyPriceCommandValidation()
        {
            RuleFor(r => r.PropertyId)
                .NotEmpty();
            RuleFor(r => r.Price)
                .GreaterThanOrEqualTo(0);
        }
    }

    public class UpdatePropertyPriceCommandHandler : IRequestHandler<UpdatePropertyPriceCommandRequest, bool>
    {
        private readonly IRepository<Property> _propertyRepo;

        public UpdatePropertyPriceCommandHandler(IRepository<Property> propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }
        public async Task<bool> Handle(UpdatePropertyPriceCommandRequest request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepo.GetByIdAsync(request.PropertyId, cancellationToken) ?? throw new ApiException("Property doesn't exist");
            property.Price = request.Price;
            _propertyRepo.Update(property);
            var result = await _propertyRepo.SaveAsync();
            if (result)
                return result;
            else
                throw new ApiException("Price could not be update");
        }
    }
}
