using FCG.Application.Dtos.Promotions;

namespace FCG.Application.Services;

public interface IPromotionService
{
    Task<PromotionDto> CreateAsync(CreatePromotionRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PromotionDto>> ListAsync(CancellationToken cancellationToken = default);
}
