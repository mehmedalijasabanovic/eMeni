using eMeni.Application.Modules.Business.BusinessCategory.Commands.Create;
using eMeni.Application.Modules.Business.BusinessCategory.Commands.Delete;
using eMeni.Application.Modules.Business.Queries.List;
using eMeni.Application.Modules.Location.Queries.List;

namespace eMeni.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class BusinessesCategoryController(ISender sender):ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateBusinessesCategory(CreateBusinessesCategoryCommand command,CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return Created(string.Empty,new { id});
        }
        [HttpDelete("{id:int}")] 
        public async Task Delete(int id,CancellationToken ct)
        {
            await sender.Send(new DeleteBusinessesCategoryCommand { Id = id },ct);
        }
        [HttpGet]
        public async Task<PageResult<ListBusinessesCategoryQueryDto>> List([FromQuery] ListBusinessesCategoryQuery query, CancellationToken ct)
        {
            return await sender.Send(query, ct);
        }

    }
}
