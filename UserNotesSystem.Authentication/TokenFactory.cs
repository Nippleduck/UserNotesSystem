using Microsoft.Extensions.Options;
using UserNotesSystem.Authentication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserNotesSystem.Authentication
{
    public sealed class TokenFactory(IOptions<TokenOptions> tokenOptions)
    {
        public async Task<string> CreateTokenAsync(string userId, string role)
        {
            var options = tokenOptions.Value;
            var tokenId = await options.GenerateJti();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role)
            };

            var jwt = new JwtSecurityToken(
                claims: claims,
                signingCredentials: options.SigningCredentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedToken;
        }
    }
}
