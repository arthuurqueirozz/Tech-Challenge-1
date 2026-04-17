using FCG.Domain.Dtos.Models.Sales;
using FCG.Domain.Entities;

namespace FCG.Infrastructure.Mappers;

public static class SaleMapper
{
    public static SaleDto ToDto(Sale sale) =>
        new()
        {
            Id = sale.Id,
            Title = sale.Title,
            Code = sale.Code,
            DiscountPercent = sale.DiscountPercent,
            ValidFrom = sale.ValidFrom,
            ValidUntil = sale.ValidUntil,
            GameId = sale.GameId,
            CreatedAt = sale.CreatedAt
        };
}
