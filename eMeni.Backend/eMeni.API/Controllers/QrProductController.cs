using eMeni.Application.Common;
using eMeni.Application.Modules.Order.QrProduct.Queries.List;

namespace eMeni.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class QrProductController(ISender sender) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<PageResult<ListQrProductQueryDto>> List([FromQuery] ListQrProductQuery query, CancellationToken ct)
        {
            return await sender.Send(query, ct);
        }
    }
}
