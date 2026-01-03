using eMeni.Application.Common;
using eMeni.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Business.BusinessCategory.Queries.List
{
    public sealed class ListBusinessesCategoryQueryHandler(IAppDbContext db):
        IRequestHandler<ListBusinessesCategoryQuery,PageResult<ListBusinessesCategoryQueryDto>>
    {
        public async Task<PageResult<ListBusinessesCategoryQueryDto>> Handle(ListBusinessesCategoryQuery query,CancellationToken ct)
        {
            var quer = db.BusinessesCategories.AsNoTracking();
            if (!query.Search.isNullOrWhiteSpace())
                quer=quer.Where(x=>x.CategoryName.ToLower()==query.Search.ToLower());
            var projectedQuery = quer.
                OrderBy(x => x.Id).
                Select(x => new ListBusinessesCategoryQueryDto { 
                    Id = x.Id, 
                    CategoryName = x.CategoryName,
                    CategoryDescription=x.CategoryDescription });

            return await PageResult<ListBusinessesCategoryQueryDto>.FromQueryableAsync(projectedQuery, query.Paging, ct);
        }
       
    }
}
