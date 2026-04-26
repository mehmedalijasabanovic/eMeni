using eMeni.Application.Common.Exceptions;
using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Identity.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler(
        IAppCurrentUser currentUser,
        IAppDbContext db,
        IAuthorizationHelper auth) : IRequestHandler<ChangePasswordCommand, Unit>
    {
        public async Task<Unit> Handle(ChangePasswordCommand command, CancellationToken ct)
        {
            auth.EnsureAuthenticated();

            if (command.Id != currentUser.UserId)
                throw new eMeniConflictException("Cannot change someone else's password.");

            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == command.Id, ct);
            if (user == null)
                throw new eMeniNotFoundException("User not found.");

            var hasher = new PasswordHasher<eMeni.Infrastructure.Models.eMeniUserEntity>();
            var verifyResult = hasher.VerifyHashedPassword(user, user.PasswordHash, command.CurrentPassword);
            if (verifyResult == PasswordVerificationResult.Failed)
                throw new eMeniConflictException("Current password is incorrect.");

            user.PasswordHash = hasher.HashPassword(user, command.NewPassword);
            await db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
