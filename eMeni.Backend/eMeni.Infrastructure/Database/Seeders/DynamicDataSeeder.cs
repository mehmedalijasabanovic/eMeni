using eMeni.Infrastructure.Models;

namespace eMeni.Infrastructure.Database.Seeders;

/// <summary>
/// Dynamic seeder koji se pokreće u runtime-u,
/// obično pri startu aplikacije (npr. u Program.cs).
/// Koristi se za unos demo/test podataka koji nisu dio migracije.
/// </summary>
public static class DynamicDataSeeder
{
    public static async Task SeedAsync(DatabaseContext context)
    {
        // Osiguraj da baza postoji (bez migracija)
        await context.Database.EnsureCreatedAsync();

        await SeedCitysAsync(context);
        await SeedUsersAsync(context);
        await SeedBusinessCategoryAsync(context);
        
        
    }

  

    /// <summary>
    /// Kreira demo korisnike ako ih još nema u bazi.
    /// </summary>
    private static async Task SeedUsersAsync(DatabaseContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        var hasher = new PasswordHasher<eMeniUserEntity>();
      
  
        var admin = new eMeniUserEntity
        {
            Email = "admin@eMeni.local",
            PasswordHash = hasher.HashPassword(null!, "Admin123!"),
            IsAdmin = true,
            CityId=1,
            FullName="Admin User",
            Phone="1234567890",
            Active=true,
            IsDeleted=false,
            CreatedAtUtc=DateTime.UtcNow,
        };

        var user = new eMeniUserEntity
        {
            Email = "manager@eMeni.local",
            PasswordHash = hasher.HashPassword(null!, "User123!"),
            IsOwner = true,
            CityId = 1,
            FullName = "Owner",
            Phone = "1234567890",
            Active = true,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        var dummyForSwagger = new eMeniUserEntity
        {
            Email = "string",
            PasswordHash = hasher.HashPassword(null!, "string"),
            IsUser = true,
            CityId = 1,
            FullName = "Admin User",
            Phone = "1234567890",
            Active = true,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };
        var dummyForTests = new eMeniUserEntity
        {
            Email = "test",
            PasswordHash = hasher.HashPassword(null!, "test123"),
            IsUser = true,
            CityId = 1,
            FullName = "Admin User",
            Phone = "1234567890",
            Active = true,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };
        context.Users.AddRange(admin, user, dummyForSwagger, dummyForTests);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ Dynamic seed: demo users added.");
    }
    private static async Task SeedCitysAsync(DatabaseContext context)
    {
        if (await context.Cities.AnyAsync())
        {
            return;
        }
        var Konjic = new CityEntity
        {
            CityName = "Konjic",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        var Mostar = new CityEntity
        {
            CityName = "Mostar",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        var Sarajevo = new CityEntity
        {
            CityName = "Sarajevo",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        context.Cities.AddRange(Konjic, Mostar, Sarajevo);
        await context.SaveChangesAsync();
        Console.WriteLine("✅ Dynamic seed: demo cities added.");
    }
    private static async Task SeedBusinessCategoryAsync(DatabaseContext context)
    {
        if (await context.BusinessesCategories.AnyAsync())
        {
            return;
        }
        var ugostiteljstvo = new BusinessesCategoryEntity
        {
            CategoryName = "Ugostiteljstvo",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        var rentAVan = new BusinessesCategoryEntity
        {
            CategoryName = "Rent a Van",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        var trgovina = new BusinessesCategoryEntity
        {
            CategoryName = "Trgovine",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        context.BusinessesCategories.AddRange(ugostiteljstvo,rentAVan,trgovina);
        await context.SaveChangesAsync();
        Console.WriteLine("✅ Dynamic seed: demo business categories added.");
    }
}