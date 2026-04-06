using FCG.Application.Dtos.Games;
using FCG.Application.Mapping;
using FCG.Domain.Entities;
using FCG.Domain.Repositories;

namespace FCG.Application.Services;

public sealed class GameCatalogService : IGameCatalogService
{
    private readonly IGameRepository _gameRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GameCatalogService(IGameRepository gameRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GameDto> CreateAsync(CreateGameRequest request, CancellationToken cancellationToken = default)
    {
        var game = Game.Create(request.Title, request.Description, request.Developer);
        await _gameRepository.AddAsync(game, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return GameMapper.ToDto(game);
    }

    public async Task<IReadOnlyList<GameDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        var games = await _gameRepository.ListAsync(cancellationToken);
        return games.Select(GameMapper.ToDto).ToList();
    }

    public async Task<GameDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetByIdAsync(id, cancellationToken);
        return game is null ? null : GameMapper.ToDto(game);
    }
}
