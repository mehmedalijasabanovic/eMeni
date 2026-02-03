using eMeni.Domain.Entities.Business;
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
        await SeedPackageAsync(context);
        await SeedBusinesseesAsync(context);
        
        
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
        var ownerforswagger = new eMeniUserEntity
        {
            Email = "string1",
            PasswordHash = hasher.HashPassword(null!, "string"),
            IsUser = true,
            IsOwner=true,
            CityId = 1,
            FullName = "Owner user",
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
            CategoryDescription="Lorem ipsum test test test test test test tetst test test",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        var rentAVan = new BusinessesCategoryEntity
        {
            CategoryName = "Rent a Van",
            CategoryDescription = "Lorem ipsum test test test test test test tetst test test",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        var trgovina = new BusinessesCategoryEntity
        {
            CategoryName = "Trgovine",
            CategoryDescription = "Lorem ipsum test test test test test test tetst test test",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        context.BusinessesCategories.AddRange(ugostiteljstvo,rentAVan,trgovina);
        await context.SaveChangesAsync();
        Console.WriteLine("✅ Dynamic seed: demo business categories added.");
    }
    private static async Task SeedPackageAsync(DatabaseContext context)
    {
        if (await context.PackageEntity.AnyAsync())
        {
            return;
        }
        var test = new PackageEntity
        {
            Description = "Start paket",
            PackageName = "Start",
            MaxImages = 20,
            MaxMenus = 20,
            Price = 1,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow
        };
        context.PackageEntity.Add(test);
        await context.SaveChangesAsync();
        Console.WriteLine("✅ Dynamic seed: demo packages added.");
    }
    private static async Task SeedBusinesseesAsync(DatabaseContext context)
    {
        if (await context.Business.AnyAsync())
        {
            return;
        }

        // Get owner user (one with IsOwner = true)
        var ownerUser = await context.Users.FirstOrDefaultAsync(u => u.IsOwner == true);
        if (ownerUser == null)
        {
            Console.WriteLine("⚠️ Dynamic seed: No owner user found. Skipping business seeding.");
            return;
        }

        var businessProfile = new BusinessProfileEntity
        {
            UserId = ownerUser.Id,
            PackageId = 1

        };
        context.BusinessProfiles.Add(businessProfile);
        await context.SaveChangesAsync();
        // Business 1: Restaurant (CategoryId = 1 - Ugostiteljstvo)
        var restaurant1 = new BusinessEntity
        {
            BusinessName = "Restoran Burek",
            BusinessCategoryId = 1,
            Description = "Tradicionalni restoran sa domaćom kuhinjom i autentičnim bosanskim jelima.",
            Address = "Trg 1, Konjic",
            BusinessProfileId=businessProfile.Id,
            PromotionRank = 0,
            CityId = 1,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        // Business 2: Rent a Van (CategoryId = 2)
        var rentAVan = new BusinessEntity
        {
            BusinessName = "Quick Van Rent",
            BusinessCategoryId = 2,
            Description = "Pružamo usluge iznajmljivanja kombija za prijevoz i selidbe.",
            Address = "Mostarska 15, Mostar",
            CityId = 2,
            BusinessProfileId = businessProfile.Id,
            PromotionRank = 1,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        // Business 3: Store (CategoryId = 3 - Trgovine)
        var store = new BusinessEntity
        {
            BusinessName = "Super Market Plus",
            BusinessCategoryId = 3,
            Description = "Moderna trgovina sa širokim asortimanom proizvoda svakodnevne potrebe.",
            Address = "Ferhadija 20, Sarajevo",
            CityId = 3,
            BusinessProfileId = businessProfile.Id,
            PromotionRank = 1,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        // Business 4: Another Restaurant (CategoryId = 1 - Ugostiteljstvo)
        var restaurant2 = new BusinessEntity
        {
            BusinessName = "Ćevabdžinica Kod Muje",
            BusinessCategoryId = 1,
            Description = "Najbolji ćevapi u gradu! Tradicionalna ćevabdžinica sa dugom tradicijom.",
            Address = "Stari Grad 5, Sarajevo",
            CityId = 3,
            BusinessProfileId = businessProfile.Id,
            PromotionRank = 2,
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        context.Business.AddRange(restaurant1, rentAVan, store, restaurant2);
        await context.SaveChangesAsync();

        // Add menus for businesses
        // Menu for Restaurant 1
        var menu1 = new MenuEntity
        {
            BusinessId = restaurant1.Id,
            MenuTitle = "Glavni Meni",
            MenuDescription = "Ponuda tradicionalnih bosanskih jela i pića.",
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        // Menu for Rent a Van (services menu)
        var menu2 = new MenuEntity
        {
            BusinessId = rentAVan.Id,
            MenuTitle = "Usluge Iznajmljivanja",
            MenuDescription = "Pregled dostupnih vozila i cjenovnika usluga.",
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        // Menu for Store (product catalog)
        var menu3 = new MenuEntity
        {
            BusinessId = store.Id,
            MenuTitle = "Katalog Proizvoda",
            MenuDescription = "Pregled dostupnih proizvoda i cijena.",
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        // Menu for Restaurant 2
        var menu4 = new MenuEntity
        {
            BusinessId = restaurant2.Id,
            MenuTitle = "Meni Ćevabdžinice",
            MenuDescription = "Specijaliteti naše ćevabdžinice - ćevapi, pljeskavice i više.",
            IsDeleted = false,
            CreatedAtUtc = DateTime.UtcNow,
        };

        context.Menus.AddRange(menu1, menu2, menu3, menu4);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ Dynamic seed: demo businesses and menus added.");
    }
}