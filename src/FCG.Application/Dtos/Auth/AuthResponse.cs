using FCG.Application.Dtos.Users;

namespace FCG.Application.Dtos.Auth;

public sealed class AuthResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserDto User { get; set; } = null!;
}
