using eMeni.Application.Common;

namespace eMeni.Application.Modules.Order.QrProduct.Queries.List
{
    public sealed class ListQrProductQueryHandler(IAppDbContext db)
        : IRequestHandler<ListQrProductQuery, PageResult<ListQrProductQueryDto>>
    {
        public async Task<PageResult<ListQrProductQueryDto>> Handle(ListQrProductQuery query, CancellationToken ct)
        {
            var products = db.QrProducts
                .AsNoTracking()
                .Select(x => new ListQrProductQueryDto
                {
                    Id = x.Id,
                    ProductName = x.ProductName,
                    Description = x.Description,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    MaterialType = x.MaterialType,
                    Size = x.Size
                });

            return await PageResult<ListQrProductQueryDto>.FromQueryableAsync(products, query.Paging, ct);
        }
    }
}
