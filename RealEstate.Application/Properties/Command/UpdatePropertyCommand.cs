using FluentValidation;
using MediatR;
using RealEstate.Application.Contracts;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Properties.Command
{
    public record UpdatePropertyCommandRequest : IRequest<bool>
    {
        public int PropertyId { get; init; }
        public string? Name { get; init; }
        public string? Address { get; init; }
        public int Year { get; init; }
        public int OwnerId { get; init; }
    }

    public class UpdatePropertyCommandValidation : AbstractValidator<UpdatePropertyCommandRequest>
    {
        private readonly IRepository<Owner> _ownerRepo;

        public UpdatePropertyCommandValidation(IRepository<Owner> ownerRepo)
        {
            _ownerRepo = ownerRepo;
            RuleFor(r => r.PropertyId)
                .NotEmpty();
            RuleFor(r => r.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(r => r.Address)
                .NotEmpty()
                .MaximumLength(255);
            RuleFor(r => r.Year)
                .GreaterThan(0);
            RuleFor(x => x.OwnerId)
                .NotEmpty()
                .MustAsync(OwnerExist).WithMessage("{PropertyName} doesn't exist");
        }
        public async Task<bool> OwnerExist(int ownerId, CancellationToken cancellationToken)
            => await _ownerRepo.GetByIdAsync(ownerId, cancellationToken) != null;
    }

    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommandRequest, bool>
    {
        private readonly IRepository<Property> _propertyRepo;

        public UpdatePropertyCommandHandler(IRepository<Property> propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }
        public async Task<bool> Handle(UpdatePropertyCommandRequest request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepo.GetByIdAsync(request.PropertyId, cancellationToken) ?? throw new ApiException("Property doesn't exist");
            property.Name = request.Name;
            property.Address = request.Address;
            property.Year = request.Year;
            property.OwnerId = request.OwnerId;
            _propertyRepo.Update(property);
            var result = await _propertyRepo.SaveAsync(cancellationToken);
            if (result)
                return result;
            else
                throw new ApiException("Property could not be update");
        }
    }
}
