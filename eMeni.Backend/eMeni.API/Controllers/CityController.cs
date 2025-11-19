using eMeni.Application.Modules.Location.Commands.Create;
using eMeni.Application.Modules.Location.Commands.Delete;
using eMeni.Application.Modules.Location.Queries.List;

namespace eMeni.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController(ISender sender):ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateCity(CreateCityCommand command,CancellationToken ct)
        {
            int id = await sender.Send(command, ct);

            return Created(string.Empty, new { id });
        }
        [HttpDelete("{id:int}")]
        public async Task Delete(int id, CancellationToken ct)
        {
            await sender.Send(new DeleteCityCommand { Id = id }, ct);
        }
        [HttpGet]

        public async Task<PageResult<ListCityQueryDto>> List([FromQuery] ListCityQuery query,CancellationToken ct)
        {
            return await sender.Send(query, ct);
        }
    }
}
