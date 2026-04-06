using FCG.Domain.Enums;
using FCG.Domain.Exceptions;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private User()
    {
    }

    public const int MaxNameLength = 200;

    public static User Create(string name, Email email, string passwordHash, UserRole role)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Name is required.");
        if (name.Trim().Length > MaxNameLength)
            throw new DomainValidationException($"Name must be at most {MaxNameLength} characters.");

        return new User
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email,
            PasswordHash = passwordHash,
            Role = role,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void PromoteToAdmin()
    {
        Role = UserRole.Admin;
    }

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainValidationException("Name is required.");
        if (name.Trim().Length > MaxNameLength)
            throw new DomainValidationException($"Name must be at most {MaxNameLength} characters.");

        Name = name.Trim();
    }
}
