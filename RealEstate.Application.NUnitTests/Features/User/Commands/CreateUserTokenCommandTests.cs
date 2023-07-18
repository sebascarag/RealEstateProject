using Moq;
using NUnit.Framework;
using RealEstate.Application.Contracts;
using RealEstate.Application.Features.User.Commands;
using System.Security.Authentication;

namespace RealEstate.Application.NUnitTests.Features.User.Commands
{
    public class CreateUserTokenCommandTests
    {
        private Mock<IIdentityService> _identityServiceMock;
        private Mock<IJwtProvider> _jwtProviderMock;

        [Test]
        public async Task CreateUserTokenCommand_InputCreateUserTokenRequest_ReturnsString()
        {
            // Assert
            _identityServiceMock = new Mock<IIdentityService>();
            _identityServiceMock.Setup(x => x.CheckPasswordAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);
            _jwtProviderMock = new Mock<IJwtProvider>();
            _jwtProviderMock.Setup(x => x.GenerateAsync(It.IsAny<string>())).ReturnsAsync("token");
            var request = new CreateUserTokenCommandRequest
            {
                UserName = "user@test.com",
                Password = "xxxxxxxxxx"
            };
            var handler = new CreateUserTokenCommandHanlder(_jwtProviderMock.Object, _identityServiceMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.TypeOf<string>());
                Assert.AreEqual("token", result);
            });
        }

        [Test]
        public void CreateUserTokenCommand_InputUserOrPasswordWrong_ReturnsAuthenticationException()
        {
            // Assert
            _identityServiceMock = new Mock<IIdentityService>();
            _identityServiceMock.Setup(x => x.CheckPasswordAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);
            _jwtProviderMock = new Mock<IJwtProvider>();
            _jwtProviderMock.Setup(x => x.GenerateAsync(It.IsAny<string>())).ReturnsAsync("token");
            var request = new CreateUserTokenCommandRequest
            {
                UserName = "user@test.com",
                Password = "passwrong"
            };
            var handler = new CreateUserTokenCommandHanlder(_jwtProviderMock.Object, _identityServiceMock.Object);

            // Act y Assert
            var exception = Assert.ThrowsAsync<AuthenticationException>(() => handler.Handle(request, CancellationToken.None));
            Assert.AreEqual("UserName or Password not valid", exception!.Message);
        }
    }
}
