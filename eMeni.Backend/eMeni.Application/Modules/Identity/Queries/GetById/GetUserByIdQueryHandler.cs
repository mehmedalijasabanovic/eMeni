namespace eMeni.Application.Modules.Identity.Queries.GetById
{
    public sealed class GetUserByIdQueryHandler(IAppDbContext db,IAuthorizationHelper auth) : 
        IRequestHandler<GetUserByIdQuery, GetUserByIdQueryDto>
    {
        public async Task<GetUserByIdQueryDto> Handle(GetUserByIdQuery query,CancellationToken ct)
        {
            auth.EnsureAuthenticated();
            var user= await db.Users.FirstOrDefaultAsync(x=>x.Id==query.Id,ct);
            if (user == null){throw new eMeniNotFoundException("Invalid user ID.");}
            var city = db.Cities.FirstOrDefault(x => x.Id == user.CityId);
            var queryUser = new GetUserByIdQueryDto { 
                Id = user.Id,
                Email=user.Email,
                Phone=user.Phone,
                City=city.CityName,
                FullName=user.FullName
            };
            return queryUser;
        }
    }
}
