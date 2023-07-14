using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using RealEstate.Application.Contracts;
using RealEstate.Application.Exceptions;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Properties.Command
{
    public record CreatePropertyImageCommandRequest : IRequest<bool>
    {
        public int PropertyId { get; init; }
        public IFormFile FormFile { get; init; }
        public bool Enabled { get; init; }
    }

    public class CreatePropertyImageCommandValidation : AbstractValidator<CreatePropertyImageCommandRequest>
    {
        private readonly IRepository<Property> _propertyRepo;

        public CreatePropertyImageCommandValidation(IRepository<Property> propertyRepo)
        {
            _propertyRepo = propertyRepo;
            RuleFor(r => r.FormFile)
                .NotNull().WithMessage("File not provided")
                .Must(r => r.Length < 1024 * 1024).WithMessage("The file exceeds the maximum size, must be 1 MB")
                .Must(AllowedExtension).WithMessage("File extension not allowed");
            RuleFor(r => r.PropertyId)
                .NotEmpty()
                .MustAsync(PropertyExist).WithMessage("{PropertyName} doesn't exist");
        }
        public bool AllowedExtension(IFormFile file)
        {
            string[] allowedExtension = new string[] { ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(file.FileName);
            return allowedExtension.Contains(extension.ToLower());
        }
        public async Task<bool> PropertyExist(int Id, CancellationToken cancellationToken)
            => true;//await _propertyRepo.GetByIdAsync(Id, cancellationToken) != null;
    }

    public class CreatePropertyImageCommandHandler : IRequestHandler<CreatePropertyImageCommandRequest, bool>
    {
        private readonly IRepository<PropertyImage> _propertyImageRepo;
        private readonly IFileService _fileService;

        public CreatePropertyImageCommandHandler(IRepository<PropertyImage> propertyImageRepo, IFileService fileService)
        {
            _propertyImageRepo = propertyImageRepo;
            _fileService = fileService;
        }

        public async Task<bool> Handle(CreatePropertyImageCommandRequest request, CancellationToken cancellationToken)
        {
            string fileName = "";
            try
            {
                fileName = await _fileService.SaveFileAsync(request.FormFile);

                var propetyImage = new PropertyImage
                {
                    PropertyId = request.PropertyId,
                    File = fileName,
                    Enable = request.Enabled,
                    Active = true
                };
                _propertyImageRepo.Add(propetyImage);
                var result = await _propertyImageRepo.SaveAsync(cancellationToken);
                if (result)
                    return result;
                else
                    throw new ApiException("Property image could not be save");
            }
            catch (Exception)
            {
                await _fileService.DeleteFileAsync(fileName);
                throw;
            }
        }
    }
}
