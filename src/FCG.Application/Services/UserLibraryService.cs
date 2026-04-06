using FCG.Application.Dtos.Games;
using FCG.Application.Mapping;
using FCG.Domain.Entities;
using FCG.Domain.Exceptions;
using FCG.Domain.Repositories;

namespace FCG.Application.Services;

public sealed class UserLibraryService : IUserLibraryService
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserGameRepository _userGameRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserLibraryService(
        IUserRepository userRepository,
        IGameRepository gameRepository,
        IUserGameRepository userGameRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _userGameRepository = userGameRepository;
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
        return entries.Select(e => GameMapper.ToDto(e.Game)).ToList();
    }
}
