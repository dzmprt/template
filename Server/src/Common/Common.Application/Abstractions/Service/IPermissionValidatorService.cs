namespace Common.Application.Abstractions.Service;

public interface IPermissionValidatorService
{
    /// <summary>
    /// Validate current user in role. Throw exception if user don't have role.
    /// </summary>
    /// <param name="roleId">Role to validate.</param>
    void ValidateRole(int roleId);
}