using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Identity.Commands.Delete
{
    public sealed class DeleteUserCommandHandler(IAppDbContext db,IAuthorizationHelper auth):IRequestHandler<DeleteUserCommand,Unit>
    {
        public async Task<Unit> Handle(DeleteUserCommand command,CancellationToken ct)
        {
            auth.EnsureAdmin();
            bool exists=await db.Users.AnyAsync(x=>x.Id==command.Id,ct);

            if (!exists) { throw new eMeniNotFoundException("Invalid user ID."); }
            var user = await db.Users.FirstOrDefaultAsync(x=>x.Id== command.Id,ct);
            db.Users.Remove(user);
            await db.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
