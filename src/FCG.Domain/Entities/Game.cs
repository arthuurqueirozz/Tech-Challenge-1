using FCG.Domain.Exceptions;

namespace FCG.Domain.Entities;

public class Game
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public string? Developer { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Game()
    {
    }

    public const int MaxTitleLength = 300;

    public static Game Create(string title, string? description = null, string? developer = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainValidationException("Title is required.");
        if (title.Trim().Length > MaxTitleLength)
            throw new DomainValidationException($"Title must be at most {MaxTitleLength} characters.");

        return new Game
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
            Developer = string.IsNullOrWhiteSpace(developer) ? null : developer.Trim(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Deactivate() => IsActive = false;

    public void Update(string title, string? description, string? developer)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainValidationException("Title is required.");
        if (title.Trim().Length > MaxTitleLength)
            throw new DomainValidationException($"Title must be at most {MaxTitleLength} characters.");

        Title = title.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Developer = string.IsNullOrWhiteSpace(developer) ? null : developer.Trim();
    }
}
