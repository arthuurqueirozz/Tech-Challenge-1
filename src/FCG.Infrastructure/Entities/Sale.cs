using FCG.Domain.Shared;

namespace FCG.Infrastructure.Entities;

public class Sale
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Code { get; private set; }
    public decimal DiscountPercent { get; private set; }
    public DateTime ValidFrom { get; private set; }
    public DateTime ValidUntil { get; private set; }
    public Guid GameId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Sale()
    {
    }

    public static Sale Create(
        string title,
        decimal discountPercent,
        DateTime validFrom,
        DateTime validUntil,
        Guid gameId,
        string? code = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainValidationException("Title is required.");
        if (discountPercent < 0 || discountPercent > 100)
            throw new DomainValidationException("Discount must be between 0 and 100.");
        if (validUntil <= validFrom)
            throw new DomainValidationException("ValidUntil must be after ValidFrom.");

        return new Sale
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

    public bool IsActiveAt(DateTime utcNow) => ValidFrom <= utcNow && utcNow < ValidUntil;

    public bool Overlaps(DateTime validFrom, DateTime validUntil) => ValidFrom < validUntil && validFrom < ValidUntil;

    public decimal CalculateCurrentPrice(decimal fullPrice)
    {
        if (fullPrice < 0)
            throw new DomainValidationException("Price must be greater than or equal to zero.");

        var discountFactor = 1 - (DiscountPercent / 100m);
        return decimal.Round(fullPrice * discountFactor, 2, MidpointRounding.AwayFromZero);
    }
}
