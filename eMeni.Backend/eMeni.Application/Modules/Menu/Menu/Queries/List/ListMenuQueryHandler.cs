using eMeni.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.Menu.Queries.List
{
    public sealed class ListMenuQueryHandler(IAppDbContext db):IRequestHandler<ListMenuQuery,PageResult<ListMenuQueryDto>>
    {
        public async Task<PageResult<ListMenuQueryDto>> Handle(ListMenuQuery query, CancellationToken ct)
        {
            var menus = db.Menus.AsNoTracking();
                

            var projectedQuery = menus
                .OrderBy(x => x.MenuTitle)
                .Select(x => new ListMenuQueryDto
                {
                    Id = x.Id,
                    MenuTitle = x.MenuTitle,
                    MenuDescription = x.MenuDescription,
                    PromotionRank = x.PromotionRank,
                    Categories = x.MenuCategories
                        .Where(c => !c.IsDeleted)
                        .OrderBy(c => c.OrderIndex)
                        .Select(c => new MenuCategoryDto
                        {
                            Id = c.Id,
                            CategoryName = c.CategoryName,
                            OrderIndex = c.OrderIndex,
                            Description = c.Description,
                            Items = c.MenuItems
                                .Where(i => !i.IsDeleted)
                                .Select(i => new MenuItemDto
                                {
                                    Id = i.Id,
                                    ItemName = i.ItemName,
                                    ItemDescription = i.ItemDescription,
                                    Price = i.Price,
                                    ImageUrl = i.ImageUrl
                                })
                                .ToList()
                        })
                        .ToList()
                });

            return await PageResult<ListMenuQueryDto>
                .FromQueryableAsync(projectedQuery, query.Paging, ct);
        }
    }
}
