using Common.Application.Mappings;
using UM.Domain;

namespace Common.ApiAuth.DTOs;

public class UserInfoDto : IMapFrom<ApplicationUser>
{
    public string Id { get; set; }
    
    public string Login { get; set; }
    
    public int[] RoleIds { get; set; }
    
    public UserInfoDto(ApplicationUser applicationUser)
    {
        Id = applicationUser.ApplicationUserId;
        Login = applicationUser.Login;
        RoleIds = applicationUser.ApplicationUserRoles.Select(r => r.ApplicationUserRoleId).ToArray();
    }
}