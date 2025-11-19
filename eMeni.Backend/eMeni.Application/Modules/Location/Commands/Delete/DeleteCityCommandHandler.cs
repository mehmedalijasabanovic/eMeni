using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Location.Commands.Delete
{
    public class DeleteCityCommandHandler(IAppDbContext db, IAppCurrentUser currentUser):
        IRequestHandler<DeleteCityCommand,Unit>
    {
        public async Task<Unit> Handle(DeleteCityCommand command,CancellationToken ct)
        {
            if (currentUser.UserId is null)
                throw new eMeniBusinessRuleException("1", "User is not authenticated");

            var city= await db.Cities.FirstOrDefaultAsync(x=>x.Id == command.Id,ct);

            if (city is null)
                throw new eMeniNotFoundException("City with that Id doesnt exist.");
            
            city.IsDeleted = true;
            await db.SaveChangesAsync(ct);
                
            return Unit.Value;
        }
    }
}
