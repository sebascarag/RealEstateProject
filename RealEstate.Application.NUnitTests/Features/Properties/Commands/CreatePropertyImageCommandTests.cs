using AutoFixture;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using RealEstate.Application.Contracts;
using RealEstate.Application.Features.Properties.Commands;
using RealEstate.Application.NUnitTests.Mocks;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.NUnitTests.Features.Properties.Commands
{
    [TestFixture]
    public class CreatePropertyImageCommandTests
    {
        private IRepository<PropertyImage> _propertyImageRepoMock;
        private Mock<IFileService> _fileServiceMock;
        private Mock<IFormFile> _formFileMock;

        [SetUp]
        public void SetUp()
        {
            _propertyImageRepoMock = MockRepository<PropertyImage>.GetMockIRepository();
        }

        [Test]
        public async Task CreatePropertyImageCommand_InputPropertyImageRequest_ReturnsTrue()
        {
            // Arrange
            _formFileMock = new Mock<IFormFile>();
            _formFileMock.Setup(f => f.FileName).Returns("test-image.png");

            _fileServiceMock = new Mock<IFileService>();
            _fileServiceMock.Setup(f => f.SaveFileAsync(It.IsAny<IFormFile>())).ReturnsAsync(_formFileMock.Object.FileName);

            var request = new CreatePropertyImageCommandRequest
            {
                PropertyId = 1,
                FormFile = _formFileMock.Object,
                Enabled = true
            };
            CreatePropertyImageCommandHandler handler = new(_propertyImageRepoMock, _fileServiceMock.Object);

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedData = _propertyImageRepoMock.GetAllActive().Last();


            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<bool>());
                Assert.True(result);
                Assert.That(savedData.PropertyId, Is.EqualTo(request.PropertyId));
                Assert.That(savedData.File, Is.EqualTo(request.FormFile.FileName));
                Assert.That(savedData.Enable, Is.EqualTo(request.Enabled));
            });
        }

        [Test]
        public void CreatePropertyImageCommand_InputEmptyFile_ReturnsException()
        {
            // Arrange
            _fileServiceMock = new Mock<IFileService>();
            _fileServiceMock.Setup(f => f.SaveFileAsync(It.IsAny<IFormFile>())).ThrowsAsync(new Exception());
            var request = new CreatePropertyImageCommandRequest();
            CreatePropertyImageCommandHandler handler = new(_propertyImageRepoMock, _fileServiceMock.Object);

            // Act y Assert
            var exception = Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
