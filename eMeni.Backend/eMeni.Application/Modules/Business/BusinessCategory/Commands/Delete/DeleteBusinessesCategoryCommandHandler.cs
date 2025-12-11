using eMeni.Shared.Constants;

namespace eMeni.Application.Modules.Business.BusinessCategory.Commands.Delete
{
    public class DeleteBusinessesCategoryCommandHandler(IAppDbContext db,IAppCurrentUser user) : IRequestHandler<DeleteBusinessesCategoryCommand,Unit>
    {
        public async Task<Unit> Handle(DeleteBusinessesCategoryCommand command,CancellationToken ct)
        {
            if (!user.IsAuthenticated)
            {
                throw new eMeniBusinessRuleException(Messages.NotAuthenticatedCode, Messages.NotAuthenticated);
            }else if (!user.IsAdmin)
            {
                throw new eMeniBusinessRuleException(Messages.NotAuthorizedCode, Messages.NotAuthorized);
            }

            var ctg = await db.BusinessesCategories.FirstOrDefaultAsync(x => x.Id == command.Id, ct);
            if (ctg == null)
                throw new eMeniNotFoundException("This business category doesnt exist.");
            else if (ctg.Businesses.Any())
                throw new eMeniBusinessRuleException("CATEGORY_HAS_BUSINESSES", "Cannot delete category that has businesses");
            
                db.BusinessesCategories.Remove(ctg);
            await db.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
