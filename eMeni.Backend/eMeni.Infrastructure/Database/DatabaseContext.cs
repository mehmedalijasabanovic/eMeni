using eMeni.Application.Abstractions;
using eMeni.Infrastructure.Models;

namespace eMeni.Infrastructure.Database;

public partial class DatabaseContext : DbContext, IAppDbContext
{

    public DbSet<eMeniUserEntity> Users => Set<eMeniUserEntity>();
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();
    public DbSet<CityEntity> Cities => Set<CityEntity>();

    private readonly TimeProvider _clock;
    public DatabaseContext(DbContextOptions<DatabaseContext> options, TimeProvider clock) : base(options)
    {
        _clock = clock;
    }
}