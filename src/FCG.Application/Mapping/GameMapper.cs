using FCG.Application.Dtos.Games;
using FCG.Domain.Entities;

namespace FCG.Application.Mapping;

public static class GameMapper
{
    public static GameDto ToDto(Game game) =>
        new()
        {
            Id = game.Id,
            Title = game.Title,
            Description = game.Description,
            Developer = game.Developer,
            IsActive = game.IsActive,
            CreatedAt = game.CreatedAt
        };
}
