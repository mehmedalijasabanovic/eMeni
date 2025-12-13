using eMeni.Application.Common;
using eMeni.Shared.Helpers;

namespace eMeni.Application.Modules.Business.Business.Queries.List
{
    public sealed class ListBusinessQueryHandler(IAppDbContext db):IRequestHandler<ListBusinessQuery,PageResult<ListBusinessQueryDto>>
    {
     public async Task<PageResult<ListBusinessQueryDto>> Handle(ListBusinessQuery query,CancellationToken ct)
        {

            var businesses = db.Business.AsNoTracking();
            if (!query.City.isNullOrWhiteSpace())
            {
                businesses = businesses.
                    Where(x => x.City.CityName.ToLower().Trim() == query.City.ToLower().Trim());
            }
            var projectedQuery = businesses.OrderBy(x => x.BusinessName).
                Select(x => new ListBusinessQueryDto
                {
                    Id = x.Id,
                    BusinessName = x.BusinessName,
                    Description = x.Description,
                    Address = x.Address
                });
            return await PageResult<ListBusinessQueryDto>.FromQueryableAsync(projectedQuery,query.Paging,ct);
        }
    }
}
