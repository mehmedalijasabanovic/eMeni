using eMeni.Application.Modules.Location.Commands.Create;

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
    }
}
