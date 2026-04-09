using FCG.Domain.Dtos.Models.Catalog;
using FCG.Domain.Interfaces;
using FCG.Domain.Shared;
using FCG.Infrastructure.Entities;
using FCG.Infrastructure.Interfaces;
using FCG.Infrastructure.Mappers;

namespace FCG.Application.Services;

public sealed class UserLibraryService : IUserLibraryService
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserGameRepository _userGameRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserLibraryService(
        IUserRepository userRepository,
        IGameRepository gameRepository,
        IUserGameRepository userGameRepository,
        ISaleRepository saleRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _userGameRepository = userGameRepository;
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AcquireGameAsync(Guid userId, Guid gameId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            throw new DomainValidationException("User not found.");

        var game = await _gameRepository.GetByIdAsync(gameId, cancellationToken);
        if (game is null || !game.IsActive)
            throw new DomainValidationException("Game not found.");

        if (await _userGameRepository.ExistsAsync(userId, gameId, cancellationToken))
            throw new DomainConflictException("Game is already in your library.");

        var userGame = UserGame.Create(userId, gameId);
        await _userGameRepository.AddAsync(userGame, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<GameDto>> GetMyGamesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var entries = await _userGameRepository.GetByUserIdAsync(userId, cancellationToken);
        var utcNow = DateTime.UtcNow;
        var activeSales = await _saleRepository.ListActiveByGameIdsAsync(entries.Select(e => e.GameId).ToArray(), utcNow, cancellationToken);
        var activeSalesByGameId = activeSales
            .GroupBy(s => s.GameId)
            .ToDictionary(g => g.Key, g => g.First());

        return entries
            .Select(entry => GameMapper.ToDto(
                entry.Game,
                activeSalesByGameId.GetValueOrDefault(entry.GameId)))
            .ToList();
    }
}
