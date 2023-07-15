using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace RealEstate.Services.Authentication.Configs
{
    public class JwtOptionsConfig : IConfigureOptions<JwtOptions>
    {
        private const string SectionName = "Jwt";
        private readonly IConfiguration _configuration;
        public JwtOptionsConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Configure(JwtOptions options)
        {
            // bind Jwt section from appsettings.json to JwtOptions class
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
