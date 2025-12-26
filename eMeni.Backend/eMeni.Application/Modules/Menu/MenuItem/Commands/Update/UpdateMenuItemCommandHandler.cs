using MediatR;

namespace eMeni.Application.Modules.Menu.MenuItem.Commands.Update
{
    public sealed class UpdateMenuItemCommandHandler(IAppDbContext db,IAuthorizationHelper auth) : IRequestHandler<UpdateMenuItemCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateMenuItemCommand command,CancellationToken ct)
        {
            auth.EnsureOwner();
            var item = await db.MenuItems.FirstOrDefaultAsync(x => x.Id == command.Id, ct)
            ?? throw new eMeniNotFoundException("This menu item doesnt exist");

            item.ItemName = command.ItemName;
            item.ItemDescription = command.ItemDescription;
            item.Price = command.Price;
            item.ImageUrl = command.ImageUrl;

            await db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
