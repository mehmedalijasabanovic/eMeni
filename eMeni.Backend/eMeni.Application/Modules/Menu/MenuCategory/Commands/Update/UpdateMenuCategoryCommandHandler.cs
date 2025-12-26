using MediatR;

namespace eMeni.Application.Modules.Menu.MenuCategory.Commands.Update
{
    public sealed class UpdateMenuCategoryCommandHandler(IAppDbContext db,IAuthorizationHelper auth) : IRequestHandler<UpdateMenuCategoryCommand, Unit> 
    {
        public async Task<Unit> Handle(UpdateMenuCategoryCommand command,CancellationToken ct)
        {
            auth.EnsureOwner();
            var category = await db.MenuCategories.FirstOrDefaultAsync(x => x.Id == command.Id, ct)
            ?? throw new eMeniNotFoundException("This category doesnt exist");

            category.CategoryName = command.CategoryName;
            category.OrderIndex = command.OrderIndex;
            category.Description = command.Description;

            await db.SaveChangesAsync(ct);
            return Unit.Value;
        }
    
    }

}
