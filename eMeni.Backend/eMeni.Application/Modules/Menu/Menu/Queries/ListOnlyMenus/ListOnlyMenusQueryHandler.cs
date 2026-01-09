using eMeni.Application.Common;
using eMeni.Application.Modules.Menu.Menu.Queries.List;
using eMeni.Shared.Helpers;

namespace eMeni.Application.Modules.Menu.Menu.Queries.ListOnlyMenus
{
    public sealed class ListOnlyMenusQueryHandler(IAppDbContext db):
        IRequestHandler<ListOnlyMenusQuery,PageResult<ListOnlyMenusQueryDto>>
    {
        public async Task<PageResult<ListOnlyMenusQueryDto>> Handle(ListOnlyMenusQuery query,CancellationToken ct)
        {
            var q=db.Menus.Include(x=>x.Business).ThenInclude(b=>b.City).Where(x=>x.Business.BusinessCategoryId==query.CategoryId).AsNoTracking();
            if (!query.City.isNullOrWhiteSpace())
            {
                q = q.Where(x => 
                x.Business.City.CityName.ToLower() == query.City.ToLower());
            }
            var projectedQuery = q.Select(x => new ListOnlyMenusQueryDto
            {
                Id = x.Id,
                MenuDescription=x.MenuDescription,
                MenuTitle=x.MenuTitle,
                PromotionRank=x.PromotionRank
            }
            );
            return await PageResult<ListOnlyMenusQueryDto>
               .FromQueryableAsync(projectedQuery, query.Paging, ct);
        }
    }
}
