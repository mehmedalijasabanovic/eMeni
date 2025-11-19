using eMeni.Application.Common;

namespace eMeni.Application.Modules.Location.Queries.List
{
    public sealed class ListCityQueryHandler(IAppDbContext db) : 
        IRequestHandler<ListCityQuery,PageResult<ListCityQueryDto>>
    {
        public async Task<PageResult<ListCityQueryDto>> Handle(ListCityQuery request, CancellationToken ct)
        {
            var q = db.Cities.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
                q=q.Where(x => x.CityName == request.Search);
            var projectedQuery = q.OrderBy(x => x.CityName).Select(x => new ListCityQueryDto { Id = x.Id, Name = x.CityName });

            return await PageResult<ListCityQueryDto>.FromQueryableAsync(projectedQuery,request.Paging,ct);
        }
    }
}
