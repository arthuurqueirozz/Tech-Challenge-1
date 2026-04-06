using FCG.Application.Dtos.Games;

namespace FCG.Application.Services;

public interface IGameCatalogService
{
    Task<GameDto> CreateAsync(CreateGameRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GameDto>> ListAsync(CancellationToken cancellationToken = default);
    Task<GameDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
