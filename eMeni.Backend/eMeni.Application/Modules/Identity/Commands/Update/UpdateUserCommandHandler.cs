using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Identity.Commands.Update
{
    public sealed class UpdateUserCommandHandler(IAppCurrentUser user,IAppDbContext db,IAuthorizationHelper auth):IRequestHandler<UpdateUserCommand,Unit>
    {
        public async Task<Unit> Handle(UpdateUserCommand command,CancellationToken ct)
        {
            auth.EnsureAuthenticated();
            if (command.Id!=user.UserId) {
                throw new eMeniConflictException("Cant update someone else's account.");
            }
            bool exists= await db.Users.AnyAsync(x=>x.Id==command.Id,ct);
            if (!exists) { throw new eMeniNotFoundException("Invalid user Id."); }
            var userUpdate=await db.Users.FirstOrDefaultAsync(x=>x.Id==command.Id,ct);
            userUpdate.Email=command.Email;
            userUpdate.FullName=command.FullName;
            userUpdate.Phone=command.Phone;
            await db.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
