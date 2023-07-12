using FluentValidation;
using MediatR;
using RealEstate.DataAccess.Repository;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Properties.Command
{
    public class CreatePropertyCommand
    {
        public class Request : IRequest<bool>
        {
            public string Name { get; set; }
            public string Address { get; set; }
            public decimal Price { get; set; }
            public int Year { get; set; }
            public int OwnerId { get; set; }
        }

        public class Validation : AbstractValidator<Request>
        {
            private readonly IRepository<Owner> _ownerRepo;

            public Validation(IRepository<Owner> ownerRepo)
            {
                _ownerRepo = ownerRepo;

                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required")
                    .MaximumLength(100).WithMessage("Name too large, must not exceed 100 characters");
                RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Address is required")
                    .MaximumLength(255).WithMessage("Address too large, must not exceed 255 characters"); ;
                RuleFor(x => x.Price)
                    .GreaterThanOrEqualTo(0).WithMessage("Price must be positive value");
                RuleFor(x => x.OwnerId)
                    .MustAsync(OwnerExist).WithMessage("Owner doesn't exist");
            }

            public async Task<bool> OwnerExist(int ownerId, CancellationToken cancellationToken)
            {
                return await _ownerRepo.GetByIdAsync(ownerId, cancellationToken) != null;
            }
        }

        public class Handler : IRequestHandler<Request, bool>
        {
            private readonly IRepository<Property> _propertyRepo;

            public Handler(IRepository<Property> propertyRepo)
            {
                _propertyRepo = propertyRepo;
            }

            public Task<bool> Handle(Request request, CancellationToken cancellationToken)
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
                if (_propertyRepo.Save())
                    return Task.FromResult(true);
                else
                    throw new Exception();
            }
        }
    }
}
