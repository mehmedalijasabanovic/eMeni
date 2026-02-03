namespace eMeni.Application.Modules.Business.Business.Queries.GetByUserId
{
    public sealed class GetBusinessByUserIdQueryHandler(IAppDbContext db,IAppCurrentUser user,IAuthorizationHelper auth) : IRequestHandler<GetBusinessByUserIdQuery, List<GetBusinessByUserIdQueryDto>>
    {
        public async Task<List<GetBusinessByUserIdQueryDto>> Handle(GetBusinessByUserIdQuery query, CancellationToken ct)
        {
            auth.EnsureOwner();
            var businesses = await db.Business.Include(b=>b.BusinessProfile).
                ThenInclude(bp=>bp.User).
                Where(x=>x.BusinessProfile.UserId==user.UserId)
                .AsNoTracking()
                .Select(i=>new GetBusinessByUserIdQueryDto
                {
                    Id = i.Id,
                    Address = i.Address,
                    BusinessName = i.BusinessName,
                    City=i.City.CityName,
                    Description=i.Description,
                    PromotionRank=i.PromotionRank
                })
                .ToListAsync(ct);
            return businesses;
        }
    }
}
