using Moq;
using NUnit.Framework;
using RealEstate.Application.Contracts;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.User.Commands;

namespace RealEstate.Application.NUnitTests.Features.User.Commands
{
    [TestFixture]
    public class CreateUserCommandTests
    {
        private Mock<IIdentityService> _identityServiceMock;

        [Test]
        public async Task CreateUserCommand_InputCreateUserRequest_ReturnsTrue()
        {
            // Assert
            _identityServiceMock = new Mock<IIdentityService>();
            _identityServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((true, new List<string>()));
            var request = new CreateUserCommandRequest 
            {
                UserName = "user@test.com",
                Password = "xxxxxxxxxx"
            };
            var handler = new CreateUserCommandHanlder(_identityServiceMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<bool>());
                Assert.True(result);
            });
        }

        [Test]
        public void CreateUserCommand_IIdentityServiceError_ReturnsApiException()
        {
            // Assert
            var errors = new List<string>() { "Error1", "Error2" };
            _identityServiceMock = new Mock<IIdentityService>();
            _identityServiceMock.Setup(x => x.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>()))
                                        .ReturnsAsync((false, errors));
            var request = new CreateUserCommandRequest();
            var handler = new CreateUserCommandHanlder(_identityServiceMock.Object);

            // Act y Assert
            var exception = Assert.ThrowsAsync<ApiException>(() => handler.Handle(request, CancellationToken.None));
            Assert.AreEqual(string.Join(", ", errors), exception!.Message);
        }
    }
}
