using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mirror.Application.Common.Interfaces;
using Mirror.Application.Common.Interfaces.Services;
using Mirror.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mirror.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtTokenGenerator> _logger;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider,
                                 IOptions<JwtSettings> jwtOptions,
                                 ILogger<JwtTokenGenerator> logger)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
            _logger = logger;
        }

        public string GenerateToken(User user)
        {
            _logger.LogInformation("Generating JWT token for user {UserId} ({FirstName} {LastName})", user.Id, user.FirstName, user.LastName);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                    SecurityAlgorithms.HmacSha256
                );

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            _logger.LogInformation("Claims generated for JWT token: {Claims}", string.Join(", ", claims.Select(c => $"{c.Type}: {c.Value}")));

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            _logger.LogInformation("JWT token generated successfully for user {UserId}. Token expires at {ExpiryTime}", user.Id, securityToken.ValidTo);

            return token;
        }
    }
}