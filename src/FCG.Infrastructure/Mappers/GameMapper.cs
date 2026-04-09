using FCG.Domain.Dtos.Models.Catalog;
using FCG.Infrastructure.Entities;

namespace FCG.Infrastructure.Mappers;

public static class GameMapper
{
    public static GameDto ToDto(Game game, Sale? activeSale = null)
    {
        var currentPrice = activeSale?.CalculateCurrentPrice(game.Price) ?? game.Price;

        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            Developer = game.Developer,
            FullPrice = game.Price,
            CurrentPrice = currentPrice,
            IsOnSale = activeSale is not null,
            IsActive = game.IsActive,
            CreatedAt = game.CreatedAt
        };
    }
}
