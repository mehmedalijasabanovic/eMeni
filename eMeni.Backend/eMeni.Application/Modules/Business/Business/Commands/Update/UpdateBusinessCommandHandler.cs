using eMeni.Shared.Helpers;

namespace eMeni.Application.Modules.Business.Business.Commands.Update
{
    public sealed class UpdateBusinessCommandHandler(IAppDbContext db,IAppCurrentUser user, IAuthorizationHelper auth) : IRequestHandler<UpdateBusinessCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateBusinessCommand command,CancellationToken ct)
        {
            auth.EnsureOwner();
            
            bool exist = await db.Business.Include(b=>b.BusinessProfile).ThenInclude(bp=>bp.User)
                .AnyAsync(x => x.BusinessProfile.UserId == user.UserId && 
            x.BusinessName == command.BusinessName&&
            x.Id!=command.Id,ct);
            if (exist)
            {
                throw new eMeniBusinessRuleException("BUSINESS_ALREADY_EXISTS", "User already have business with that name");
            }

            var business=await db.Business.FirstOrDefaultAsync(x=>x.Id==command.Id,ct);
        
            business.BusinessName = command.BusinessName;
            business.Address = command.Address;
            business.Description = command.Description;
 
            await db.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
