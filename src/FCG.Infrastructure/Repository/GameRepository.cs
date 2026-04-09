using FCG.Infrastructure.Entities;
using FCG.Infrastructure.Interfaces;
using FCG.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Repository;

public sealed class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Games.FirstOrDefaultAsync(g => g.Id == id, cancellationToken);

    public async Task AddAsync(Game game, CancellationToken cancellationToken = default)
    {
        await _context.Games.AddAsync(game, cancellationToken);
    }

    public Task UpdateAsync(Game game, CancellationToken cancellationToken = default)
    {
        _context.Games.Update(game);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Game>> ListAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Games
            .AsNoTracking()
            .OrderBy(g => g.Title)
            .ToListAsync(cancellationToken);
        return list;
    }
}
