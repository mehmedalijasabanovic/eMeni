using eMeni.Application.Abstractions;
using eMeni.Application.Modules.Menu.MenuItem.Commands.Create;
using eMeni.Application.Modules.Menu.MenuItem.Commands.Delete;
using eMeni.Application.Modules.Menu.MenuItem.Commands.Update;
using eMeni.Infrastructure.Database;
using eMeni.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eMeni.Tests.MenuItemTests.UnitTests;

// ─── Helpers ────────────────────────────────────────────────────────────────

/// <summary>No-op stub – all authorization checks pass silently.</summary>
internal sealed class FakeAuthorizationHelper : IAuthorizationHelper
{
    public void EnsureAuthenticated() { }
    public void EnsureAdmin() { }
    public void EnsureOwner() { }
}

/// <summary>Stub that throws UnauthorizedAccessException on every check.</summary>
internal sealed class UnauthorizedAuthorizationHelper : IAuthorizationHelper
{
    public void EnsureAuthenticated() => throw new UnauthorizedAccessException();
    public void EnsureAdmin() => throw new UnauthorizedAccessException();
    public void EnsureOwner() => throw new UnauthorizedAccessException();
}

internal static class DbFactory
{
    public static DatabaseContext Create(string dbName)
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        return new DatabaseContext(options, TimeProvider.System);
    }
}

// ─── Validator Tests ─────────────────────────────────────────────────────────

public sealed class CreateMenuItemCommandValidatorTests
{
    private readonly CreateMenuItemCommandValidator _validator = new();

    [Fact]
    public void Valid_Command_Should_Pass_Validation()
    {
        var cmd = new CreateMenuItemCommand
        {
            CategoryId = 1,
            ItemName = "Margherita Pizza",
            Price = "8.99"
        };

        var result = _validator.Validate(cmd);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Empty_ItemName_Should_Fail_Validation()
    {
        var cmd = new CreateMenuItemCommand
        {
            CategoryId = 1,
            ItemName = "",
            Price = "8.99"
        };

        var result = _validator.Validate(cmd);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(cmd.ItemName));
    }

    [Fact]
    public void ItemName_Exceeding_MaxLength_Should_Fail_Validation()
    {
        var cmd = new CreateMenuItemCommand
        {
            CategoryId = 1,
            ItemName = new string('A', MenuItemEntity.MenuItemConstraints.ItemNameMaxLength + 1),
            Price = "8.99"
        };

        var result = _validator.Validate(cmd);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(cmd.ItemName));
    }

    [Fact]
    public void Empty_Price_Should_Fail_Validation()
    {
        var cmd = new CreateMenuItemCommand
        {
            CategoryId = 1,
            ItemName = "Burger",
            Price = ""
        };

        var result = _validator.Validate(cmd);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(cmd.Price));
    }

    [Fact]
    public void Zero_CategoryId_Should_Fail_Validation()
    {
        var cmd = new CreateMenuItemCommand
        {
            CategoryId = 0,
            ItemName = "Burger",
            Price = "5.00"
        };

        var result = _validator.Validate(cmd);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(cmd.CategoryId));
    }
}

public sealed class UpdateMenuItemCommandValidatorTests
{
    private readonly UpdateMenuItemCommandValidator _validator = new();

    [Fact]
    public void Valid_Update_Command_Should_Pass_Validation()
    {
        var cmd = new UpdateMenuItemCommand
        {
            Id = 1,
            ItemName = "Grilled Chicken",
            ItemDescription = "Served with fries",
            Price = "12.50"
        };

        var result = _validator.Validate(cmd);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Empty_ItemName_Should_Fail_Validation()
    {
        var cmd = new UpdateMenuItemCommand
        {
            Id = 1,
            ItemName = "",
            Price = "5.00"
        };

        var result = _validator.Validate(cmd);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(cmd.ItemName));
    }

    [Fact]
    public void ItemDescription_Exceeding_MaxLength_Should_Fail_Validation()
    {
        var cmd = new UpdateMenuItemCommand
        {
            Id = 1,
            ItemName = "Salad",
            Price = "4.00",
            ItemDescription = new string('X', MenuItemEntity.MenuItemConstraints.ItemDescriptionMaxLength + 1)
        };

        var result = _validator.Validate(cmd);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == nameof(cmd.ItemDescription));
    }
}

// ─── Handler Tests ────────────────────────────────────────────────────────────

public sealed class DeleteMenuItemCommandHandlerTests
{
    [Fact]
    public async Task Delete_NonExistent_Item_Should_Throw_NotFoundException()
    {
        await using var db = DbFactory.Create(nameof(Delete_NonExistent_Item_Should_Throw_NotFoundException));
        var handler = new DeleteMenuItemCommandHandler(db, new FakeAuthorizationHelper());

        var cmd = new DeleteMenuItemCommand { Id = 999 };

        await Assert.ThrowsAsync<eMeni.Application.Common.Exceptions.eMeniNotFoundException>(
            () => handler.Handle(cmd, CancellationToken.None));
    }

    [Fact]
    public async Task Delete_Existing_Item_Should_Soft_Delete_It()
    {
        await using var db = DbFactory.Create(nameof(Delete_Existing_Item_Should_Soft_Delete_It));

        var item = new MenuItemEntity
        {
            ItemName = "Test Item",
            Price = "3.00",
            IsDeleted = false
        };
        db.MenuItems.Add(item);
        await db.SaveChangesAsync(CancellationToken.None);

        var handler = new DeleteMenuItemCommandHandler(db, new FakeAuthorizationHelper());
        var cmd = new DeleteMenuItemCommand { Id = item.Id };

        var result = await handler.Handle(cmd, CancellationToken.None);

        Assert.Equal(Unit.Value, result);

        var deleted = await db.MenuItems.FindAsync(item.Id);
        Assert.NotNull(deleted);
        Assert.True(deleted!.IsDeleted);
    }

    [Fact]
    public async Task Delete_Should_Enforce_Owner_Authorization()
    {
        await using var db = DbFactory.Create(nameof(Delete_Should_Enforce_Owner_Authorization));
        var handler = new DeleteMenuItemCommandHandler(db, new UnauthorizedAuthorizationHelper());
        var cmd = new DeleteMenuItemCommand { Id = 1 };

        // EnsureOwner throws before any DB access
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => handler.Handle(cmd, CancellationToken.None));
    }
}

public sealed class UpdateMenuItemCommandHandlerTests
{
    [Fact]
    public async Task Update_NonExistent_Item_Should_Throw_NotFoundException()
    {
        await using var db = DbFactory.Create(nameof(Update_NonExistent_Item_Should_Throw_NotFoundException));
        var handler = new UpdateMenuItemCommandHandler(db, new FakeAuthorizationHelper());

        var cmd = new UpdateMenuItemCommand
        {
            Id = 404,
            ItemName = "Nothing",
            Price = "0.00"
        };

        await Assert.ThrowsAsync<eMeni.Application.Common.Exceptions.eMeniNotFoundException>(
            () => handler.Handle(cmd, CancellationToken.None));
    }

    [Fact]
    public async Task Update_Existing_Item_Should_Persist_New_Values()
    {
        await using var db = DbFactory.Create(nameof(Update_Existing_Item_Should_Persist_New_Values));

        var item = new MenuItemEntity
        {
            ItemName = "Old Name",
            Price = "5.00",
            ItemDescription = "Old description",
            ImageUrl = null
        };
        db.MenuItems.Add(item);
        await db.SaveChangesAsync(CancellationToken.None);

        var handler = new UpdateMenuItemCommandHandler(db, new FakeAuthorizationHelper());
        var cmd = new UpdateMenuItemCommand
        {
            Id = item.Id,
            ItemName = "New Name",
            ItemDescription = "New description",
            Price = "9.99",
            ImageUrl = "https://example.com/image.png"
        };

        var result = await handler.Handle(cmd, CancellationToken.None);

        Assert.Equal(Unit.Value, result);

        var updated = await db.MenuItems.FindAsync(item.Id);
        Assert.NotNull(updated);
        Assert.Equal("New Name", updated!.ItemName);
        Assert.Equal("New description", updated.ItemDescription);
        Assert.Equal("9.99", updated.Price);
        Assert.Equal("https://example.com/image.png", updated.ImageUrl);
    }
}
