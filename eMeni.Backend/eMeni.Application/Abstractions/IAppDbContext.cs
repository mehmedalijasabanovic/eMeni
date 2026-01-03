using eMeni.Infrastructure.Models;
using System.ComponentModel.Design;

namespace eMeni.Application.Abstractions;

// Application layer
public interface IAppDbContext
{
  
    DbSet<eMeniUserEntity> Users { get; }
    DbSet<RefreshTokenEntity> RefreshTokens { get; }
    DbSet<CityEntity> Cities { get; }
    DbSet<BusinessesCategoryEntity> BusinessesCategories { get; }
    DbSet<BusinessEntity> Business { get; }
    DbSet<MenuEntity> Menus { get; }
    DbSet<MenuCategoryEntity> MenuCategories { get; }
    DbSet<MenuItemEntity> MenuItems { get; }
    DbSet<PackageEntity> PackageEntity { get; }
    Task<int> SaveChangesAsync(CancellationToken ct);
}