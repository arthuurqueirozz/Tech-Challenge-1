using FCG.Domain.Interfaces;
using FCG.Domain.Shared;
using FCG.Domain.Entities;
using FCG.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Game> Games => Set<Game>();
    public DbSet<UserGame> UserGames => Set<UserGame>();
    public DbSet<Sale> Sales => Set<Sale>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
