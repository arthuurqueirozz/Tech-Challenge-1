using FCG.Infrastructure.Entities;

namespace FCG.Infrastructure.Interfaces;

public interface ISaleRepository
{
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Sale>> ListAsync(CancellationToken cancellationToken = default);
    Task<Sale?> GetActiveByGameIdAsync(Guid gameId, DateTime utcNow, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Sale>> ListActiveByGameIdsAsync(
        IReadOnlyCollection<Guid> gameIds,
        DateTime utcNow,
        CancellationToken cancellationToken = default);
    Task<bool> HasOverlappingSaleAsync(
        Guid gameId,
        DateTime validFrom,
        DateTime validUntil,
        CancellationToken cancellationToken = default);
}
