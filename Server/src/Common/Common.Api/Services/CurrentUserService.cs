using System.Security.Claims;
using Common.Application.Abstractions.Service;
using Microsoft.AspNetCore.Http;
using UM.Domain.Constants;

namespace Common.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
        _httpContextAccessor = httpContextAccessor;

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    
    public int[]? RolesIds => _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(c => UserRoles.GetByName(c.Value).Id).ToArray();
}