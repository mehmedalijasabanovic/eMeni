using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Menu.MenuCategory.Commands.Create
{
    public sealed class CreateMenuCategoryCommandHandler(IAppDbContext db,IAuthorizationHelper auth):
        IRequestHandler<CreateMenuCategoryCommand,int>
    {
        public async Task<int> Handle(CreateMenuCategoryCommand cmd,CancellationToken ct)
        {
            auth.EnsureOwner();
            var menuExists = await db.Menus
            .AnyAsync(x => x.Id == cmd.MenuId && !x.IsDeleted, ct);

            if (!menuExists)
                throw new eMeniNotFoundException("Menu not found.");

            var category = new MenuCategoryEntity
            {
                MenuId = cmd.MenuId,
                CategoryName = cmd.CategoryName.Trim(),
                OrderIndex = cmd.OrderIndex,
                Description = cmd.Description?.Trim()
            };

            db.MenuCategories.Add(category);
            await db.SaveChangesAsync(ct);

            return category.Id;
        }
    }
}
