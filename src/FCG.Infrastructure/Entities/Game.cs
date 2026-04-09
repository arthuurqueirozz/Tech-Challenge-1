using FCG.Domain.Shared;

namespace FCG.Infrastructure.Entities;

public class Game
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public string? Developer { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Game()
    {
    }

    public const int MaxTitleLength = 300;

    public static Game Create(decimal price, string title, string? description = null, string? developer = null)
    {
        Validate(title, price);

        return new Game
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim(),
            Developer = string.IsNullOrWhiteSpace(developer) ? null : developer.Trim(),
            Price = price,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void Deactivate() => IsActive = false;

    public void Update(decimal price, string title, string? description, string? developer)
    {
        Validate(title, price);

        Title = title.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Developer = string.IsNullOrWhiteSpace(developer) ? null : developer.Trim();
        Price = price;
    }

    private static void Validate(string title, decimal price)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainValidationException("Title is required.");
        if (title.Trim().Length > MaxTitleLength)
            throw new DomainValidationException($"Title must be at most {MaxTitleLength} characters.");
        if (price < 0)
            throw new DomainValidationException("Price must be greater than or equal to zero.");
    }
}
