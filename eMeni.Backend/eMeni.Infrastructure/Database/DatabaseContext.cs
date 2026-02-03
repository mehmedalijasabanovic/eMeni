using eMeni.Application.Abstractions;
using eMeni.Domain.Entities.Business;
using eMeni.Infrastructure.Models;

namespace eMeni.Infrastructure.Database;

public partial class DatabaseContext : DbContext, IAppDbContext
{

    public DbSet<eMeniUserEntity> Users => Set<eMeniUserEntity>();
    public DbSet<RefreshTokenEntity> RefreshTokens => Set<RefreshTokenEntity>();
    public DbSet<CityEntity> Cities => Set<CityEntity>();
    public DbSet<BusinessesCategoryEntity> BusinessesCategories => Set<BusinessesCategoryEntity>();
    public DbSet<BusinessEntity> Business => Set<BusinessEntity>();
    public DbSet<MenuEntity> Menus => Set<MenuEntity>();
    public DbSet<MenuCategoryEntity> MenuCategories => Set<MenuCategoryEntity>();
    public DbSet<MenuItemEntity> MenuItems => Set<MenuItemEntity>();
    public DbSet<PackageEntity> PackageEntity => Set<PackageEntity>();
    public DbSet<BusinessProfileEntity> BusinessProfiles => Set<BusinessProfileEntity>();

    private readonly TimeProvider _clock;
    public DatabaseContext(DbContextOptions<DatabaseContext> options, TimeProvider clock) : base(options)
    {
        _clock = clock;
    }
}