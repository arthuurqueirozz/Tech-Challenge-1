using FCG.Domain.Entities;

namespace FCG.Domain.Repositories;

public interface IUserGameRepository
{
    Task<bool> ExistsAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default);
    Task AddAsync(UserGame userGame, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<UserGame>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}
