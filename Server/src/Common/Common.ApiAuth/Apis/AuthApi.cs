using Common.Api;
using Common.ApiAuth.Abstractions;
using Common.ApiAuth.DTOs;
using Common.Application.Abstractions;
using Common.Application.Abstractions.Persistence.Repository.Read;
using Common.Application.Abstractions.Service;
using Common.Application.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UM.Domain;

namespace Common.ApiAuth.Apis;

public class AuthApi : IApi
{
    public void Register(WebApplication app, string baseApiUrl = null)
    {
        app.MapPost(baseApiUrl + "/login", Login)
            .WithName("Login")
            .WithTags("Authorization");

        app.MapGet(baseApiUrl + "/current-user-info", CurrentUserInfo)
            .WithName("Current user info")
            .WithTags("Authorization");
    }

    [Authorize]
    private async Task<IResult> CurrentUserInfo(ICurrentUserService currentUserService,
        IBaseReadRepository<ApplicationUser> users, CancellationToken cancellationToken)
    {
        var user = await users.SingleAsync(u => u.ApplicationUserId == currentUserService.UserId, cancellationToken);
        return Results.Ok(new UserInfoDto(user));
    }
    
    [AllowAnonymous]
    private async Task<IResult> Login(
        [FromBody] LoginDto loginDto,
        ITokenService tokenService,
        IBaseReadRepository<ApplicationUser> users,
        IConfiguration configuration,
        CancellationToken cancellation)
    {
        var user = await users.Where(u => u.Login == loginDto.Login).SingleOrDefaultAsync(cancellation);
        if (user != null)
        {
            if (PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                var token = tokenService.BuildToken(
                    configuration["Jwt:Key"]!,
                    configuration["Jwt:Issuer"]!,
                    user);
                return Results.Ok(token);
            }
        }

        return Results.Unauthorized();
    }
}