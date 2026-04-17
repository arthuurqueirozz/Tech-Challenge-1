using FCG.Domain.Dtos.Models.Catalog;

namespace FCG.Domain.Interfaces;

public interface IUserLibraryService
{
    Task AcquireGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GameDto>> GetMyGamesAsync(Guid userId, CancellationToken cancellationToken = default);
}
