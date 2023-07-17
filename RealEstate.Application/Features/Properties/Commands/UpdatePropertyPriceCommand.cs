using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using RealEstate.Application.Contracts;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Constants;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.Properties.Commands
{
    [Authorize(Roles = Roles.Administrator)]
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
            var result = await _propertyRepo.SaveAsync(cancellationToken);
            if (result)
                return result;
            else
                throw new ApiException("Price could not be update");
        }
    }
}
