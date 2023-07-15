using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstate.Application.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RealEstate.Services.Authentication
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IIdentityService _identityService;
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(IIdentityService identityService, IOptions<JwtOptions> jwtOptions)
        {
            _identityService = identityService;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<string> GenerateAsync(string userName)
        {
            var claims = await _identityService.GetUserClaimsAsync(userName);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims,
                notBefore: null,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials
               );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
