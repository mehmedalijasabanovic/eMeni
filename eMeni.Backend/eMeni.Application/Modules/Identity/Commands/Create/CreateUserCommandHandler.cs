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
            var hasher=new PasswordHasher<eMeniUserEntity>();
            var newUser = new eMeniUserEntity
            {
                FullName = command.FullName,
                CityId = command.CityId,
                Phone = command.Phone,
                Email = command.Email,
                PasswordHash = hasher.HashPassword(null!, command.PasswordHash),
                TokenVersion = 0,
                CreatedAtUtc = DateTime.UtcNow
            };
            db.Users.Add(newUser);
            await db.SaveChangesAsync(ct);
            return newUser.Id;
        }
    }
}
