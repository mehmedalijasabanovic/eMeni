namespace eMeni.Application.Modules.Business.Business.Commands.Delete
{
    public sealed class DeleteBusinessCommandHandler(IAppDbContext db,IAppCurrentUser user,IAuthorizationHelper auth) : IRequestHandler<DeleteBusinessCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteBusinessCommand command,CancellationToken ct)
        {
            auth.EnsureOwner();//Checks if user is authenticated and if user have owner role
            var business=await db.Business.FirstOrDefaultAsync(x => x.Id == command.Id,ct);

            if(business == null) { throw new eMeniBusinessRuleException("INVALID_BUSINESS", "This business doesnt exist."); }
            db.Business.Remove(business);
            await db.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
