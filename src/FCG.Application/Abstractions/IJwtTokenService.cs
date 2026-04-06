namespace FCG.Application.Abstractions;

public sealed record JwtTokenResult(string AccessToken, DateTime ExpiresAt);

public interface IJwtTokenService
{
    JwtTokenResult CreateToken(Guid userId, string email, string role);
}
