using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Business.BusinessCategory.Commands.Create
{
    public class CreateBusinessesCategoryCommandHandler(IAppDbContext db):IRequestHandler<CreateBusinessesCategoryCommand,int>
    {
        public async Task<int> Handle(CreateBusinessesCategoryCommand command,CancellationToken ct)
        {
            var bc = command.Name?.Trim();
            if (string.IsNullOrWhiteSpace(bc))
                throw new ValidationException("Name is required.");
            bool exists = await db.BusinessesCategories.AnyAsync(x => x.CategoryName.ToLower() == bc.ToLower(), ct);
            if (exists)
                throw new eMeniConflictException("That category already exists");
            var newcategory = new BusinessesCategoryEntity
            {
                CategoryName = bc,
                CreatedAtUtc = DateTime.UtcNow,
            };
            db.BusinessesCategories.Add(newcategory);
            await db.SaveChangesAsync(ct);
            return newcategory.Id;
        } 
    }
}
