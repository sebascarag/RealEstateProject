using Moq;
using NUnit.Framework;
using RealEstate.Application.Contracts;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Features.User.Commands;

namespace RealEstate.Application.NUnitTests.Features.User.Commands
{
    [TestFixture]
    public class AddUserAdminRoleCommandTests
    {
        private Mock<IIdentityService> _identityServiceMock;

        [Test] 
        public async Task AddUserAdminRoleCommand_InputAddUserAdminRoleRequest_ReturnsTrue()
        {
            // Assert
            _identityServiceMock = new Mock<IIdentityService>();
            _identityServiceMock.Setup(x => x.AddUserToRoleAync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((true, new List<string>()));
            var request = new AddUserAdminRoleCommandRequest("user@test.com");
            var handler = new AddUserAdminRoleCommandHanlder(_identityServiceMock.Object);

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
        public void AddUserAdminRoleCommand_IIdentityServiceError_ReturnsApiException()
        {
            // Assert
            var errors = new List<string>() { "Error1", "Error2" };
            _identityServiceMock = new Mock<IIdentityService>();
            _identityServiceMock.Setup(x => x.AddUserToRoleAync(It.IsAny<string>(), It.IsAny<string>()))
                                        .ReturnsAsync((false, errors));
            var request = new AddUserAdminRoleCommandRequest("user@test.com");
            var handler = new AddUserAdminRoleCommandHanlder(_identityServiceMock.Object);

            // Act y Assert
            var exception = Assert.ThrowsAsync<ApiException>(() => handler.Handle(request, CancellationToken.None));
            Assert.AreEqual(string.Join(", ", errors), exception!.Message);
        }
    }
}
