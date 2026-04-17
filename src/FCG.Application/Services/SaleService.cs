using FCG.Domain.Dtos.Models.Sales;
using FCG.Domain.Interfaces;
using FCG.Domain.Shared;
using FCG.Domain.Entities;
using FCG.Infrastructure.Interfaces;
using FCG.Infrastructure.Mappers;

namespace FCG.Application.Services;

public sealed class SaleService : ISaleService
{
    private readonly IGameRepository _gameRepository;
    private readonly ISaleRepository _saleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SaleService(IGameRepository gameRepository, ISaleRepository saleRepository, IUnitOfWork unitOfWork)
    {
        _gameRepository = gameRepository;
        _saleRepository = saleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<SaleDto> CreateAsync(CreateSaleRequest request, CancellationToken cancellationToken = default)
    {
        var game = await _gameRepository.GetByIdAsync(request.GameId, cancellationToken);
        if (game is null || !game.IsActive)
            throw new DomainValidationException("Game not found.");

        if (await _saleRepository.HasOverlappingSaleAsync(
                request.GameId,
                request.ValidFrom,
                request.ValidUntil,
                cancellationToken))
        {
            throw new DomainConflictException("There is already an active sale configured for this game and period.");
        }

        var sale = Sale.Create(
            request.Title,
            request.DiscountPercent,
            request.ValidFrom,
            request.ValidUntil,
            request.GameId,
            request.Code);
        await _saleRepository.AddAsync(sale, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return SaleMapper.ToDto(sale);
    }

    public async Task<IReadOnlyList<SaleDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        var items = await _saleRepository.ListAsync(cancellationToken);
        return items.Select(SaleMapper.ToDto).ToList();
    }
}
