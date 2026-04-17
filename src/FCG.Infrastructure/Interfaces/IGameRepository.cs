using FCG.Domain.Entities;

namespace FCG.Infrastructure.Interfaces;

public interface IGameRepository
{
    Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Game game, CancellationToken cancellationToken = default);
    Task UpdateAsync(Game game, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Game>> ListAsync(CancellationToken cancellationToken = default);
}
