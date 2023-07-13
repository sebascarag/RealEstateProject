using FluentValidation;
using MediatR;
using RealEstate.Application.Contracts;
using RealEstate.Application.Wrappers;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Properties.Command
{
    public record CreatePropertyCommandRequest : IRequest<bool>
    {
        public string Name { get; init; }
        public string Address { get; init; }
        public decimal Price { get; init; }
        public int Year { get; init; }
        public int OwnerId { get; init; }
    }

    public class CreatePropertyCommandValidation : AbstractValidator<CreatePropertyCommandRequest>
    {
        private readonly IRepository<Owner> _ownerRepo;

        public CreatePropertyCommandValidation(IRepository<Owner> ownerRepo)
        {
            _ownerRepo = ownerRepo;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} too large, must not exceed 100 characters");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(255).WithMessage("{PropertyName} too large, must not exceed 255 characters");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be positive value");
            RuleFor(x => x.OwnerId)
                .MustAsync(OwnerExist).WithMessage("{PropertyName} doesn't exist");
        }

        public async Task<bool> OwnerExist(int ownerId, CancellationToken cancellationToken)
        {
            return await _ownerRepo.GetByIdAsync(ownerId, cancellationToken) != null;
        }
    }

    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommandRequest, bool>
    {
        private readonly IRepository<Property> _propertyRepo;

        public CreatePropertyCommandHandler(IRepository<Property> propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }

        public async Task<bool> Handle(CreatePropertyCommandRequest request, CancellationToken cancellationToken)
        {
            var property = new Property
            {
                Name = request.Name,
                Address = request.Address,
                Price = request.Price,
                Year = request.Year,
                OwnerId = request.OwnerId,
                CodeInternal = Guid.NewGuid().ToString(),
                Active = true
            };
            _propertyRepo.Add(property);
            var result = await _propertyRepo.SaveAsync();
            if (result)
                return result;
            else
                throw new ApiException("Property could not be save");
        }
    }
}
