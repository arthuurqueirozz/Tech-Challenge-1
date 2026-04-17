using FCG.Application.Services;
using FCG.Domain.Dtos.Models.Sales;
using FCG.Domain.Interfaces;
using FCG.Domain.Services;
using FCG.Domain.Shared;
using FCG.Domain.Entities;
using FCG.Infrastructure.Interfaces;

namespace FCG.Application.Tests.Sales;

public sealed class SaleServiceTests
{
    [Fact]
    public async Task CreateAsync_WhenGameDoesNotExist_ShouldThrow()
    {
        var service = new SaleService(new FakeGameRepository(), new FakeSaleRepository(), new FakeUnitOfWork());

        await Assert.ThrowsAsync<DomainValidationException>(() => service.CreateAsync(new CreateSaleRequest
        {
            Title = "Weekend Sale",
            DiscountPercent = 10,
            ValidFrom = DateTime.UtcNow,
            ValidUntil = DateTime.UtcNow.AddDays(1),
            GameId = Guid.NewGuid()
        }));
    }

    [Fact]
    public async Task CreateAsync_WhenSaleOverlaps_ShouldThrow()
    {
        var game = Game.Create(100m, "Cloud Racer");
        var saleRepository = new FakeSaleRepository
        {
            HasOverlap = true
        };
        var service = new SaleService(
            new FakeGameRepository(game),
            saleRepository,
            new FakeUnitOfWork());

        await Assert.ThrowsAsync<DomainConflictException>(() => service.CreateAsync(new CreateSaleRequest
        {
            Title = "Weekend Sale",
            DiscountPercent = 10,
            ValidFrom = DateTime.UtcNow,
            ValidUntil = DateTime.UtcNow.AddDays(1),
            GameId = game.Id
        }));
    }

    private sealed class FakeGameRepository : IGameRepository
    {
        private readonly Game? _game;

        public FakeGameRepository(Game? game = null)
        {
            _game = game;
        }

        public Task<Game?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            Task.FromResult(_game is not null && _game.Id == id ? _game : null);

        public Task AddAsync(Game game, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task UpdateAsync(Game game, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<IReadOnlyList<Game>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<Game>>(_game is null ? [] : [_game]);
    }

    private sealed class FakeSaleRepository : ISaleRepository
    {
        public bool HasOverlap { get; init; }

        public Task AddAsync(Sale sale, CancellationToken cancellationToken = default) => Task.CompletedTask;

        public Task<IReadOnlyList<Sale>> ListAsync(CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<Sale>>([]);

        public Task<Sale?> GetActiveByGameIdAsync(Guid gameId, DateTime utcNow, CancellationToken cancellationToken = default) =>
            Task.FromResult<Sale?>(null);

        public Task<IReadOnlyList<Sale>> ListActiveByGameIdsAsync(
            IReadOnlyCollection<Guid> gameIds,
            DateTime utcNow,
            CancellationToken cancellationToken = default) =>
            Task.FromResult<IReadOnlyList<Sale>>([]);

        public Task<bool> HasOverlappingSaleAsync(
            Guid gameId,
            DateTime validFrom,
            DateTime validUntil,
            CancellationToken cancellationToken = default) =>
            Task.FromResult(HasOverlap);
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(1);
    }
}
