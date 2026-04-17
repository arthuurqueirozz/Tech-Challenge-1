using FCG.Domain.Entities;
using FCG.Infrastructure.Mappers;

namespace FCG.Application.Tests.Catalog;

public sealed class GameMapperTests
{
    [Fact]
    public void ToDto_WhenGameHasActiveSale_ReturnsCommercialView()
    {
        var game = Game.Create(100m, "Cloud Racer");
        var sale = Sale.Create(
            "Weekend Sale",
            20m,
            DateTime.UtcNow.AddHours(-1),
            DateTime.UtcNow.AddHours(1),
            game.Id);

        var dto = GameMapper.ToDto(game, sale);

        Assert.True(dto.IsOnSale);
        Assert.Equal(100m, dto.FullPrice);
        Assert.Equal(80m, dto.CurrentPrice);
    }
}
