using MediatR;

namespace eMeni.Application.Modules.Menu.MenuCategory.Commands.Delete
{
    public sealed class DeleteMenuCategoryCommandHandler(IAppDbContext db, IAuthorizationHelper auth) : IRequestHandler<DeleteMenuCategoryCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteMenuCategoryCommand command, CancellationToken ct)
        {
            auth.EnsureOwner();

            var category = await db.MenuCategories
            .Include(c => c.MenuItems)
            .FirstOrDefaultAsync(x => x.Id == command.Id && !x.IsDeleted, ct)
            ?? throw new eMeniNotFoundException("This menu category doesnt exist.");

            category.IsDeleted = true;

            foreach (var item in category.MenuItems)
                item.IsDeleted = true;

            await db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
