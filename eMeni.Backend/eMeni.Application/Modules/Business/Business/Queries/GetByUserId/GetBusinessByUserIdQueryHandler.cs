namespace eMeni.Application.Modules.Business.Business.Queries.GetByUserId
{
    public sealed class GetBusinessByUserIdQueryHandler(IAppDbContext db) : IRequestHandler<GetBusinessByUserIdQuery, List<GetBusinessByUserIdQueryDto>>
    {
        public async Task<List<GetBusinessByUserIdQueryDto>> Handle(GetBusinessByUserIdQuery query, CancellationToken ct)
        {
            var businesses = await db.Business.Where(x=>x.UserId==query.UserId)
                .Select(i=>new GetBusinessByUserIdQueryDto
                {
                    Id = i.Id,
                    Address = i.Address,
                    BusinessName = i.BusinessName,
                    City=i.City.CityName,
                    Description=i.Description,
                })
                .ToListAsync(ct);
            return businesses;
        }
    }
}
