using FCG.Domain.Dtos.Models.Sales;

namespace FCG.Domain.Interfaces;

public interface ISaleService
{
    Task<SaleDto> CreateAsync(CreateSaleRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SaleDto>> ListAsync(CancellationToken cancellationToken = default);
}
