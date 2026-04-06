using FCG.Application.Dtos.Games;

namespace FCG.Application.Services;

public interface IUserLibraryService
{
    Task AcquireGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GameDto>> GetMyGamesAsync(Guid userId, CancellationToken cancellationToken = default);
}
