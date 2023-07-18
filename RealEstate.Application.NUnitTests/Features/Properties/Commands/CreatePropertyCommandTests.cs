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
    public class CreatePropertyCommandTests
    {
        private IRepository<Property> _propertyRepoMock;
        private readonly Fixture _fixture;

        public CreatePropertyCommandTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [SetUp]
        public void SetUp()
        {
            _propertyRepoMock = MockRepository<Property>.GetMockIRepository();
        }

        [Test]
        public async Task CreatePropertyCommand_InputPropertyRequest_ReturnsTrue()
        {
            // Arrange
            var request = new CreatePropertyCommandRequest
            {
                Name = "Summit Properties",
                Address = "3970 Pooh Bear Lane Greenville, SC 29607",
                Price = 23690,
                Year = 2015,
                OwnerId = 1
            };
            CreatePropertyCommandHandler handler = new(_propertyRepoMock);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);
            var savedData = _propertyRepoMock.GetAllActive().Last();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<bool>());
                Assert.True(result);
                Assert.That(savedData.Name, Is.EqualTo(request.Name));
                Assert.That(savedData.Address, Is.EqualTo(request.Address));
                Assert.That(savedData.Price, Is.EqualTo(request.Price));
                Assert.That(savedData.Year, Is.EqualTo(request.Year));
                Assert.That(savedData.OwnerId, Is.EqualTo(request.OwnerId));
            });
        }

        [Test]
        public void CreatePropertyCommand_InputEmptyPropertyRequest_ReturnsApiException()
        {
            // Arrange
            var request = new CreatePropertyCommandRequest();
            CreatePropertyCommandHandler handler = new(_propertyRepoMock);

            // Act y Assert
            var exception = Assert.ThrowsAsync<ApiException>(() => handler.Handle(request, CancellationToken.None));
        }
    }
}
