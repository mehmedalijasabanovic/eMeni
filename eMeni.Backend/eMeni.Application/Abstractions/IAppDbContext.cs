using eMeni.Infrastructure.Models;

namespace eMeni.Application.Abstractions;

// Application layer
public interface IAppDbContext
{
  
    DbSet<eMeniUserEntity> Users { get; }
    DbSet<RefreshTokenEntity> RefreshTokens { get; }
    DbSet<CityEntity> Cities { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}