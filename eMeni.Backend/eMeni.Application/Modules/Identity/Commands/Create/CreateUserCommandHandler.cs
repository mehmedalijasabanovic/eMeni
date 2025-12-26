using eMeni.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Identity.Commands.Create
{
    public sealed class CreateUserCommandHandler(IAppDbContext db,IAppCurrentUser user):IRequestHandler<CreateUserCommand,int>
    {
        public async Task<int> Handle(CreateUserCommand command,CancellationToken ct)
        {
            if (user is null||user.UserId==0)
            {
                throw new eMeniConflictException("User is already logged in.");
            }
            var exists=await db.Users.AnyAsync(x=>x.Email.ToLower()==command.Email.ToLower().Trim(),ct);
            if (exists) { throw new eMeniConflictException("This email is already in use."); }

            var hasher=new PasswordHasher<eMeniUserEntity>();
            var newUser = new eMeniUserEntity
            {
                FullName = command.FullName.Trim(),
                CityId = command.CityId,
                Phone = command.Phone.Trim(),
                Email = command.Email.Trim(),
                PasswordHash = hasher.HashPassword(null!, command.PasswordHash.Trim()),
                TokenVersion = 0,
                CreatedAtUtc = DateTime.UtcNow
            };
            db.Users.Add(newUser);
            await db.SaveChangesAsync(ct);
            return newUser.Id;
        }
    }
}
