using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.Menu.Commands.UpdatePromotionRank
{
    public sealed class UpdateMenuPromotionRankCommandHandler(IAppDbContext db,IAuthorizationHelper auth):IRequestHandler<UpdateMenuPromotionRankCommand,Unit>
    {
        public async Task<Unit> Handle(UpdateMenuPromotionRankCommand command,CancellationToken ct)
        {
            auth.EnsureOwner();
            var menu = await db.Menus.FirstOrDefaultAsync(x => x.Id == command.Id, ct)
            ?? throw new eMeniNotFoundException("This menu doesnt exist.");

            menu.PromotionRank = command.PromotionRank;
            await db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
