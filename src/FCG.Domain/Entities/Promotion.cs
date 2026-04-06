using FCG.Domain.Exceptions;

namespace FCG.Domain.Entities;

public class Promotion
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Code { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public DateTime ValidFrom { get; private set; }
    public DateTime ValidUntil { get; private set; }
    public Guid? GameId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Promotion()
    {
    }

    public static Promotion Create(
        string title,
        decimal discountPercent,
        DateTime validFrom,
        DateTime validUntil,
        string? code = null,
        Guid? gameId = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainValidationException("Title is required.");
        if (discountPercent < 0 || discountPercent > 100)
            throw new DomainValidationException("Discount must be between 0 and 100.");
        if (validUntil <= validFrom)
            throw new DomainValidationException("ValidUntil must be after ValidFrom.");

        return new Promotion
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            Code = string.IsNullOrWhiteSpace(code) ? null : code.Trim(),
            DiscountPercent = discountPercent,
            ValidFrom = validFrom,
            ValidUntil = validUntil,
            GameId = gameId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
