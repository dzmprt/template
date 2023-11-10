using Common.Application.Abstractions.Service;
using Common.Application.Exceptions;

namespace Common.Application.Services;

public class PermissionValidatorService : IPermissionValidatorService
{
    private readonly ICurrentUserService _currentUserService;

    public PermissionValidatorService(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public void ValidateRole(int roleId)
    {
        if (_currentUserService.RolesIds?.Contains(roleId) == true)
        {
            return;
        }

        throw new ForbiddenException($"User not in role with id={roleId}");
    }
}