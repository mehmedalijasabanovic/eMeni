using MediatR;

namespace eMeni.Application.Modules.Menu.Menu.Commands.Update
{
    public sealed class UpdateMenuCommandHandler(IAppDbContext db,IAuthorizationHelper auth) : IRequestHandler<UpdateMenuCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateMenuCommand command,CancellationToken ct)
        {
            auth.EnsureOwner();
            var menu = await db.Menus.FirstOrDefaultAsync(x => x.Id == command.Id, ct)
            ?? throw new eMeniNotFoundException("Menu with this ID doesnt exist.");

            menu.MenuTitle = command.MenuTitle;
            menu.MenuDescription = command.MenuDescription;

            await db.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
