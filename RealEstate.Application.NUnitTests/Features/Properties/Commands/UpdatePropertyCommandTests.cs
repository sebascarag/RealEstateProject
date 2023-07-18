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
    public class UpdatePropertyCommandTests
    {
        private IRepository<Property> _propertyRepoMock;
        private readonly Fixture _fixture;

        public UpdatePropertyCommandTests()
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
                                .With(p => p.Active, true).Create();
            _propertyRepoMock.Add(property);
            await _propertyRepoMock.SaveAsync(CancellationToken.None);
        }

        [Test]
        public async Task UpdatePropertyCommand_InputPropertyRequest_ReturnsTrue()
        {
            // Arrange
            var request = new UpdatePropertyCommandRequest
            {
                PropertyId = 1,
                Name = "Summit Properties",
                Address = "3970 Pooh Bear Lane Greenville, SC 29607",
                Year = 2015,
                OwnerId = 1
            };
            UpdatePropertyCommandHandler handler = new(_propertyRepoMock);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);
            var savedData = _propertyRepoMock.GetAllActive().Last();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<bool>());
                Assert.True(result);
                Assert.That(savedData.Id, Is.EqualTo(request.PropertyId));
                Assert.That(savedData.Name, Is.EqualTo(request.Name));
                Assert.That(savedData.Address, Is.EqualTo(request.Address));
                Assert.That(savedData.Year, Is.EqualTo(request.Year));
                Assert.That(savedData.OwnerId, Is.EqualTo(request.OwnerId));
            });
        }

        [Test]
        public async Task UpdatePropertyCommand_InputDoesNotExistPropertyId_ReturnsApiException()
        {
            // Arrange
            var request = new UpdatePropertyCommandRequest
            {
                PropertyId = 20,
            };
            UpdatePropertyCommandHandler handler = new(_propertyRepoMock);

            // Act y Assert
            var exception = Assert.ThrowsAsync<ApiException>(() => handler.Handle(request, CancellationToken.None));
            Assert.AreEqual("Property doesn't exist", exception!.Message);
        }

        [Test]
        public async Task UpdatePropertyCommand_InputEmptyPropertyRequest_ReturnsApiException()
        {
            // Arrange
            var request = new UpdatePropertyCommandRequest();
            UpdatePropertyCommandHandler handler = new(_propertyRepoMock);

            // Act y Assert
            var exception = Assert.ThrowsAsync<ApiException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
