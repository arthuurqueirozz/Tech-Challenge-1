using FCG.Application.Dtos.Promotions;
using FCG.Application.Mapping;
using FCG.Domain.Entities;
using FCG.Domain.Repositories;

namespace FCG.Application.Services;

public sealed class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PromotionService(IPromotionRepository promotionRepository, IUnitOfWork unitOfWork)
    {
        _promotionRepository = promotionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PromotionDto> CreateAsync(CreatePromotionRequest request, CancellationToken cancellationToken = default)
    {
        var promotion = Promotion.Create(
            request.Title,
            request.DiscountPercent,
            request.ValidFrom,
            request.ValidUntil,
            request.Code,
            request.GameId);
        await _promotionRepository.AddAsync(promotion, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return PromotionMapper.ToDto(promotion);
    }

    public async Task<IReadOnlyList<PromotionDto>> ListAsync(CancellationToken cancellationToken = default)
    {
        var items = await _promotionRepository.ListAsync(cancellationToken);
        return items.Select(PromotionMapper.ToDto).ToList();
    }
}
