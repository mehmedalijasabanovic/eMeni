using eMeni.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Location.Commands.Create
{
    public class CreateCityCommandHandler(IAppDbContext db): IRequestHandler<CreateCityCommand,int>
    {
        public async Task<int> Handle(CreateCityCommand request, CancellationToken ct) 
        {
            var norm = request.Name?.Trim();
            if (string.IsNullOrWhiteSpace(norm))
                throw new ValidationException("Name is required.");

            bool exist= await db.Cities.AnyAsync(x=>x.CityName == norm,ct);
            if (exist)
                throw new eMeniConflictException("That name already exists.");
            var city = new CityEntity 
            { 
                CityName=norm,
                CreatedAtUtc=DateTime.UtcNow
            };

            db.Cities.Add(city);
            await db.SaveChangesAsync(ct);

            return city.Id;
            
        }
    }
}
