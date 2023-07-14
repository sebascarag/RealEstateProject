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
        public CreatePropertyImageCommandValidation()
        {
            RuleFor(r => r.FormFile)
                .NotNull().WithMessage("File not provided")
                .Must(AllowedExtension).WithMessage("File extension not allowed");
        }
        public bool AllowedExtension(IFormFile file)
        {
            string[] allowedExtension = new string[] { ".png", ".jpg", ".jpeg" };
            var extension = Path.GetExtension(file.FileName);
            return allowedExtension.Contains(extension.ToLower());
        }
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
            try
            {
                var fileName = await _fileService.SaveFileAsync(request.FormFile);
                var propetyImage = new PropertyImage
                {
                    PropertyId = request.PropertyId,
                    File = fileName,
                    Enable = request.Enabled,
                    Active = true
                };
                _propertyImageRepo.Add(propetyImage);
                var result = await _propertyImageRepo.SaveAsync();
                if (result)
                    return result;
                else
                    throw new ApiException("Property image could not be save");
            }
            catch (Exception)
            {
                throw new ApiException("Could not save file");
            }

        }
    }
}
