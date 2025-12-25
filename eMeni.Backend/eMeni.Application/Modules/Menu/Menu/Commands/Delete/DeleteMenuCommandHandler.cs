namespace eMeni.Application.Modules.Menu.Menu.Commands.Delete
{
    public sealed class DeleteMenuCommandHandler(IAppDbContext db, IAuthorizationHelper auth) : IRequestHandler<DeleteMenuCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteMenuCommand command, CancellationToken ct)
        {
            auth.EnsureOwner();
            var menu = await db.Menus
           .Include(m => m.MenuCategories)
               .ThenInclude(c => c.MenuItems)
           .FirstOrDefaultAsync(x => x.Id == command.Id && !x.IsDeleted, ct)
           ?? throw new eMeniNotFoundException("This menu doesnt exist.");

            menu.IsDeleted = true;

            foreach (var category in menu.MenuCategories)
            {
                category.IsDeleted = true;
                foreach (var item in category.MenuItems)
                    item.IsDeleted = true;
            }

            await db.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
