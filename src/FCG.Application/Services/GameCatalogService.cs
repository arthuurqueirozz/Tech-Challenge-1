using FCG.Domain.Dtos.Models.Catalog;
using FCG.Domain.Interfaces;
using FCG.Domain.Entities;
using FCG.Infrastructure.Interfaces;
using FCG.Infrastructure.Mappers;

namespace FCG.Application.Services;

public sealed class GameCatalogService : IGameCatalogService
{
    private readonly IGameRepository _gameRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GameCatalogService(
        IGameRepository gameRepository,
        ISaleRepository saleRepository,
        IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<GameDto> CreateAsync(CreateGameRequest request, CancellationToken cancellationToken = default)
    {
        var game = Game.Create(request.Price, request.Title, request.Description, request.Developer);
        await _gameRepository.AddAsync(game, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return GameMapper.ToDto(game);
    }

    public async Task<IReadOnlyList<GameDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        var games = await _gameRepository.ListAsync(cancellationToken);
        var utcNow = DateTime.UtcNow;
        var activeSales = await _saleRepository.ListActiveByGameIdsAsync(games.Select(g => g.Id).ToArray(), utcNow, cancellationToken);
        var activeSalesByGameId = activeSales
            .GroupBy(s => s.GameId)
            .ToDictionary(g => g.Key, g => g.First());

        return games
            .Select(game => GameMapper.ToDto(
                game,
                activeSalesByGameId.GetValueOrDefault(game.Id)))
            .ToList();
    }

    public async Task<GameDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetByIdAsync(id, cancellationToken);
        if (game is null)
            return null;

        var activeSale = await _saleRepository.GetActiveByGameIdAsync(game.Id, DateTime.UtcNow, cancellationToken);
        return GameMapper.ToDto(game, activeSale);
    }
}
