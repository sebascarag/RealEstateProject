using AutoFixture;
using NUnit.Framework;
using RealEstate.Application.Contracts;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.Properties.Commands;
using RealEstate.Application.NUnitTests.Mocks;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.NUnitTests.Features.Properties.Commands
{
    [TestFixture]
    public class UpdatePropertyPriceCommandTests
    {
        private IRepository<Property> _propertyRepoMock;
        private readonly Fixture _fixture;

        public UpdatePropertyPriceCommandTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [SetUp]
        public async Task SetUp()
        {
            _propertyRepoMock = MockRepository<Property>.GetMockIRepository();
            var property = _fixture.Build<Property>()
                                .With(p => p.Id, 1)
                                .With(p => p.Name, "RealEstate")
                                .With(p => p.Price, 1)
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);
        }

        [Test]
        public async Task UpdatePropertyPriceCommand_InputPropertyRequest_ReturnsTrue()
        {
            // Arrange
            var request = new UpdatePropertyPriceCommandRequest
            {
                PropertyId = 1,
                Price = 23456
            };
            UpdatePropertyPriceCommandHandler handler = new(_propertyRepoMock);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);
            var savedData = _propertyRepoMock.GetAllActive().Last();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<bool>());
                Assert.True(result);
                Assert.That(savedData.Id, Is.EqualTo(request.PropertyId));
                Assert.That(savedData.Price, Is.EqualTo(request.Price));
            });
        }

        [Test]
        public void UpdatePropertyPriceCommand_InputDoesNotExistPropertyId_ReturnsApiException()
        {
            // Arrange
            var request = new UpdatePropertyPriceCommandRequest
            {
                PropertyId = 20,
            };
            UpdatePropertyPriceCommandHandler handler = new(_propertyRepoMock);

            // Act y Assert
            var exception = Assert.ThrowsAsync<ApiException>(() => handler.Handle(request, CancellationToken.None));
            Assert.AreEqual("Property doesn't exist", exception!.Message);
        }

        [Test]
        public void UpdatePropertyPriceCommand_InputEmptyPropertyRequest_ReturnsApiException()
        {
            // Arrange
            var request = new UpdatePropertyPriceCommandRequest();
            UpdatePropertyPriceCommandHandler handler = new(_propertyRepoMock);

            // Act y Assert
            var exception = Assert.ThrowsAsync<ApiException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
