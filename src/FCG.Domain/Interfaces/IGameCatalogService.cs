using FCG.Domain.Dtos.Models.Catalog;

namespace FCG.Domain.Interfaces;

public interface IGameCatalogService
{
    Task<GameDto> CreateAsync(CreateGameRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GameDto>> ListAsync(CancellationToken cancellationToken = default);
    Task<GameDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
