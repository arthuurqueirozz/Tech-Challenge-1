using FCG.Domain.Shared;
using FCG.Domain.Entities;

namespace FCG.Domain.Tests.Sales;

public sealed class SaleTests
{
    [Fact]
    public void Create_WithInvalidWindow_Throws()
    {
        var validFrom = DateTime.UtcNow;

        Assert.Throws<DomainValidationException>(() =>
            Sale.Create("Flash Sale", 10, validFrom, validFrom, Guid.NewGuid()));
    }

    [Fact]
    public void Overlaps_WhenPeriodsIntersect_ReturnsTrue()
    {
        var sale = Sale.Create(
            "Flash Sale",
            10,
            new DateTime(2026, 04, 10, 10, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 04, 20, 10, 0, 0, DateTimeKind.Utc),
            Guid.NewGuid());

        Assert.True(sale.Overlaps(
            new DateTime(2026, 04, 15, 10, 0, 0, DateTimeKind.Utc),
            new DateTime(2026, 04, 25, 10, 0, 0, DateTimeKind.Utc)));
    }

    [Fact]
    public void CalculateCurrentPrice_WithDiscountPercent_ReturnsRoundedPrice()
    {
        var sale = Sale.Create(
            "Flash Sale",
            12.5m,
            DateTime.UtcNow.AddDays(-1),
            DateTime.UtcNow.AddDays(1),
            Guid.NewGuid());

        var currentPrice = sale.CalculateCurrentPrice(200m);

        Assert.Equal(175.00m, currentPrice);
    }
}
