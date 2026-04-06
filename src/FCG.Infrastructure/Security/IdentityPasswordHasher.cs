using FCG.Application.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace FCG.Infrastructure.Security;

public sealed class IdentityPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<object> _hasher = new();
    private static readonly object Marker = new();

    public string Hash(string password) => _hasher.HashPassword(Marker, password);

    public bool Verify(string password, string passwordHash) =>
        _hasher.VerifyHashedPassword(Marker, passwordHash, password) == PasswordVerificationResult.Success;
}
