using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.Business.Commands.UpdatePromotionRank
{
    public sealed class UpdateBusinessPromotionRankCommandHandler(IAppDbContext db,IAuthorizationHelper auth):IRequestHandler<UpdateBusinessPromotionRankCommand,Unit>
    {
        public async Task<Unit> Handle(UpdateBusinessPromotionRankCommand command,CancellationToken ct)
        {
            auth.EnsureOwner();
            var business = await db.Business.FirstOrDefaultAsync(x => x.Id == command.Id, ct)
            ?? throw new eMeniNotFoundException("This business doesnt exist.");

            business.PromotionRank = command.PromotionRank;
            await db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
