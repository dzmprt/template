using Common.ApiAuth.Models;
using UM.Domain;

namespace Common.ApiAuth.Abstractions;

public interface ITokenService
{
    JwtToken BuildToken(string key, string issuer, ApplicationUser user);
}