using MediatR;

namespace eMeni.Application.Modules.Menu.MenuItem.Commands.Delete
{
    public sealed class DeleteMenuItemCommandHandler(IAppDbContext db,IAuthorizationHelper auth):IRequestHandler<DeleteMenuItemCommand,Unit>
    {
        public async Task<Unit> Handle(DeleteMenuItemCommand command,CancellationToken ct)
        {
            auth.EnsureOwner();
            var item = await db.MenuItems
            .FirstOrDefaultAsync(x => x.Id == command.Id && !x.IsDeleted, ct)
            ?? throw new eMeniNotFoundException("This item doesnt exist.");

            item.IsDeleted = true;
            await db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
