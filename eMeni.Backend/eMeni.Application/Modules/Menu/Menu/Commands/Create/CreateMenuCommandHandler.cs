using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Menu.Menu.Commands.Create
{
    public sealed class CreateMenuCommandHandler(IAppDbContext db,IAuthorizationHelper auth) : IRequestHandler<CreateMenuCommand, int>
    {
        public async Task<int> Handle(CreateMenuCommand cmd,CancellationToken ct)
        {
            auth.EnsureOwner();
            var business = await db.Business.Include(p=>p.BusinessProfile)
                .ThenInclude(bp=>bp.Package)
                .Include(m=>m.Menus).FirstOrDefaultAsync(x => x.Id == cmd.BusinessId, ct);
            if (business.BusinessProfile.Package.MaxMenus==business.Menus.Count())
            {
                throw new eMeniConflictException("You already have max menus for this business.");
            }
            var menu = new MenuEntity
            {
                BusinessId = cmd.BusinessId,
                MenuTitle = cmd.MenuTitle.Trim(),
                MenuDescription = cmd.MenuDescription?.Trim()
            };

            db.Menus.Add(menu);
            await db.SaveChangesAsync(ct);

            return menu.Id;
        }
    }
}
