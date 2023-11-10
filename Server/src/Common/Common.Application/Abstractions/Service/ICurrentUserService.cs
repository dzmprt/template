namespace Common.Application.Abstractions.Service;

public interface ICurrentUserService
{
    string? UserId { get; }
    
    public int[]? RolesIds { get; }
}