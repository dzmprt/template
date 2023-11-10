namespace Common.ApiAuth.Models;

public record JwtToken
{
    public string AccessToken { get; set; } = default!;
    
    public string? RefreshToken { get; set; }

    public string UserId { get; set; } = default!;
    
    public long Expires { get; set; }
}