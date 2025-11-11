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

        await SeedUsersAsync(context);
    }

  

    /// <summary>
    /// Kreira demo korisnike ako ih još nema u bazi.
    /// </summary>
    private static async Task SeedUsersAsync(DatabaseContext context)
    {
        if (await context.Users.AnyAsync())
            return;

        var hasher = new PasswordHasher<eMeniUserEntity>();
        var oneCity = new CityEntity
        {
            CityName = "Demo City",
            CreatedAtUtc = DateTime.UtcNow,
            IsDeleted = false,
        };
        

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
        context.Cities.Add(oneCity);
        await context.SaveChangesAsync();

        Console.WriteLine("✅ Dynamic seed: demo users added.");
    }
}