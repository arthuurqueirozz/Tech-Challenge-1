using FCG.Domain.Shared;
using FCG.Infrastructure.Entities;

namespace FCG.Domain.Tests.Catalog;

public sealed class GameTests
{
    [Fact]
    public void Create_WithValidPrice_PersistsPrice()
    {
        var game = Game.Create(199.90m, "Cloud Racer", "Arcade", "Studio");

        Assert.Equal(199.90m, game.Price);
    }

    [Fact]
    public void Create_WithNegativePrice_Throws()
    {
        Assert.Throws<DomainValidationException>(() => Game.Create(-1m, "Cloud Racer"));
    }
}
