using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common.ApiAuth.Abstractions;
using Common.ApiAuth.Models;
using Microsoft.IdentityModel.Tokens;
using UM.Domain;

namespace Common.ApiAuth.Services;

public class TokenService : ITokenService
{
    private readonly TimeSpan _expiryDuration = new TimeSpan(24, 0, 0);

    public JwtToken BuildToken(string key, string issuer, ApplicationUser user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.NameIdentifier, user.ApplicationUserId)
        };

        foreach (var role in user.ApplicationUserRoles)
        {
            claims.Add(new(ClaimTypes.Role, role.Name));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256Signature);
        var expires = DateTime.UtcNow.Add(_expiryDuration);
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
            expires: DateTime.Now.Add(_expiryDuration), signingCredentials: credentials);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        var result = new JwtToken
        {
            AccessToken = token,
            RefreshToken = null,
            Expires = ((DateTimeOffset)expires).ToUnixTimeSeconds(),
            UserId = user.ApplicationUserId
        };
        return result;
    }
}