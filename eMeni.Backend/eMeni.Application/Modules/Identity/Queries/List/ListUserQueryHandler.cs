using eMeni.Application.Common;

namespace eMeni.Application.Modules.Identity.Queries.List
{
    public sealed class ListUserQueryHandler(IAppDbContext db,IAuthorizationHelper auth):
        IRequestHandler<ListUserQuery,PageResult<ListUserQueryDto>>
    {
        public async Task<PageResult<ListUserQueryDto>> Handle(ListUserQuery query,CancellationToken ct)
        {
            auth.EnsureAdmin();
            var users = db.Users.AsNoTracking();
            var projectedquery = users.OrderBy(x => x.Id).Select(x => new ListUserQueryDto
            {
                Email = x.Email,
                Id = x.Id,
                FullName = x.FullName,
                Phone = x.Phone
            });
            return await PageResult<ListUserQueryDto>.FromQueryableAsync(projectedquery, query.Paging, ct);
        }
    }
}
