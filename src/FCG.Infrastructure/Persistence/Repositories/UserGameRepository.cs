using FCG.Domain.Entities;
using FCG.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Persistence.Repositories;

public sealed class UserGameRepository : IUserGameRepository
{
    private readonly AppDbContext _context;

    public UserGameRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default) =>
        await _context.UserGames.AnyAsync(
            ug => ug.UserId == userId && ug.GameId == gameId,
            cancellationToken);

    public async Task AddAsync(UserGame userGame, CancellationToken cancellationToken = default)
    {
        await _context.UserGames.AddAsync(userGame, cancellationToken);
    }

    public async Task<IReadOnlyList<UserGame>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var list = await _context.UserGames
            .AsNoTracking()
            .Include(ug => ug.Game)
            .Where(ug => ug.UserId == userId)
            .OrderBy(ug => ug.AcquiredAt)
            .ToListAsync(cancellationToken);
        return list;
    }
}
