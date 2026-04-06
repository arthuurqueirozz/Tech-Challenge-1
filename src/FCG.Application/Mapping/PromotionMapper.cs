using FCG.Application.Dtos.Promotions;
using FCG.Domain.Entities;

namespace FCG.Application.Mapping;

public static class PromotionMapper
{
    public static PromotionDto ToDto(Promotion promotion) =>
        new()
        {
            Id = promotion.Id,
            Title = promotion.Title,
            Code = promotion.Code,
            DiscountPercent = promotion.DiscountPercent,
            ValidFrom = promotion.ValidFrom,
            ValidUntil = promotion.ValidUntil,
            GameId = promotion.GameId,
            CreatedAt = promotion.CreatedAt
        };
}
