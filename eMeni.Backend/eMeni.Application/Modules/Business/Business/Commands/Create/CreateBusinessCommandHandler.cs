using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Business.Business.Commands.Create
{
    public class CreateBusinessCommandHandler(IAppDbContext db,IAppCurrentUser user):IRequestHandler<CreateBusinessCommand,int>
    {
        public async Task<int> Handle(CreateBusinessCommand command, CancellationToken ct)
        {
            var businesses = db.Business.Where(x => x.UserId == user.UserId).ToList();
            if (businesses.Count()!=0) {
                var requestName = command.BusinessName?.Trim();
                if (businesses.Any(x=>x.BusinessName.ToLower().Trim()==requestName.ToLower())) {
                    throw new eMeniBusinessRuleException("1","User already have business with that name.");
                }
            }
            var business = new BusinessEntity { 
                BusinessName = command.BusinessName,
                BusinessCategoryId = command.BusinessCategoryId,
                Address = command.Address,
                CityId = command.CityId,
                CreatedAtUtc=DateTime.UtcNow,
                IsDeleted=false,
                Description = command.Description,
                PackageId = command.PackageId,
                UserId=user.UserId.Value
            };
            db.Business.Add(business);
            await db.SaveChangesAsync(ct);

            return business.Id;
        }
    }


}
