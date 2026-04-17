using FCG.Domain.Entities;
using FCG.Infrastructure.Interfaces;
using FCG.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repository;

public sealed class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
    }

    public async Task<IReadOnlyList<Sale>> ListAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Sales
            .AsNoTracking()
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);
        return list;
    }

    public async Task<Sale?> GetActiveByGameIdAsync(Guid gameId, DateTime utcNow, CancellationToken cancellationToken = default) =>
        await _context.Sales
            .AsNoTracking()
            .Where(s => s.GameId == gameId && s.ValidFrom <= utcNow && utcNow < s.ValidUntil)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<IReadOnlyList<Sale>> ListActiveByGameIdsAsync(
        IReadOnlyCollection<Guid> gameIds,
        DateTime utcNow,
        CancellationToken cancellationToken = default)
    {
        if (gameIds.Count == 0)
            return [];

        return await _context.Sales
            .AsNoTracking()
            .Where(s => gameIds.Contains(s.GameId) && s.ValidFrom <= utcNow && utcNow < s.ValidUntil)
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> HasOverlappingSaleAsync(
        Guid gameId,
        DateTime validFrom,
        DateTime validUntil,
        CancellationToken cancellationToken = default) =>
        await _context.Sales.AnyAsync(
            s => s.GameId == gameId && s.ValidFrom < validUntil && validFrom < s.ValidUntil,
            cancellationToken);
}
