using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Menu.MenuItem.Commands.Create
{
    public sealed class CreateMenuItemCommandHandler(IAppDbContext db,IAuthorizationHelper auth):
        IRequestHandler<CreateMenuItemCommand, int>
    {
        public async Task<int> Handle(CreateMenuItemCommand cmd, CancellationToken ct)
        {
            var categoryExists = await db.MenuCategories
                .AnyAsync(x => x.Id == cmd.CategoryId && !x.IsDeleted, ct);

            if (!categoryExists)
                throw new eMeniNotFoundException("Category not found.");

            var item = new MenuItemEntity
            {
                CategoryId = cmd.CategoryId,
                ItemName = cmd.ItemName.Trim(),
                ItemDescription = cmd.ItemDescription?.Trim(),
                Price = cmd.Price.Trim(),
                ImageUrl = cmd.ImageUrl
            };

            db.MenuItems.Add(item);
            await db.SaveChangesAsync(ct);

            return item.Id;
        }
    }
}
