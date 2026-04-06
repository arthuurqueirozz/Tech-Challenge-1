using FCG.Domain.Entities;
using FCG.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Persistence.Repositories;

public sealed class PromotionRepository : IPromotionRepository
{
    private readonly AppDbContext _context;

    public PromotionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Promotion promotion, CancellationToken cancellationToken = default)
    {
        await _context.Promotions.AddAsync(promotion, cancellationToken);
    }

    public async Task<IReadOnlyList<Promotion>> ListAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Promotions
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync(cancellationToken);
        return list;
    }
}
