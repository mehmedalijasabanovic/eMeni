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

            var category=await db.MenuCategories.FirstOrDefaultAsync(x=>x.Id== cmd.CategoryId,ct);
            var menu=await db.Menus.Include(b=>b.Business).ThenInclude(p=>p.Package).FirstOrDefaultAsync(x=>x.Id==category.MenuId,ct);
            
            var maxOfImages = category.Menu.Business.Package.MaxImages;
            var countOfImages=await db.MenuItems.Where(i=>i.ImageUrl!=null&&!i.IsDeleted&&i.Category.MenuId==menu.Id).CountAsync(ct);
            if (maxOfImages == countOfImages)
                throw new eMeniConflictException("You already have maximum images on this menu.");

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
