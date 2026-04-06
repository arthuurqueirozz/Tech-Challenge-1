using FCG.Domain.Entities;

namespace FCG.Domain.Repositories;

public interface IPromotionRepository
{
    Task AddAsync(Promotion promotion, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Promotion>> ListAsync(CancellationToken cancellationToken = default);
}
