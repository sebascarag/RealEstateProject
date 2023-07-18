using Moq;
using NUnit.Framework;
using RealEstate.Application.Contracts;
using RealEstate.Application.Features.User.Commands;

namespace RealEstate.Application.NUnitTests.Features.User.Commands
{
    [TestFixture]
    public class DeleteUserCommandTests
    {
        private Mock<IIdentityService> _identityServiceMock;

        [Test]
        public async Task DeleteUserCommand_InputDeleteUserRequest_ReturnsTrue()
        {
            // Assert
            _identityServiceMock = new Mock<IIdentityService>();
            _identityServiceMock.Setup(x => x.DeleteUserAsync(It.IsAny<string>())).ReturnsAsync(true);
            var request = new DeleteUserCommandRequest("user@test.com");
            var handler = new DeleteUserCommandHanlder(_identityServiceMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<bool>());
                Assert.True(result);
            });
        }
    }
}
