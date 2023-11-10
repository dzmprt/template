namespace Common.ApiAuth.DTOs;

public record JwtTokenDto
{
    public string AccessToken { get; set; } = default!;
    
    public string? RefreshToken { get; set; }

    public string UserId { get; set; } = default!;
    
    public int Expires { get; set; }
    
    public int Lifetime { get; set; }
}