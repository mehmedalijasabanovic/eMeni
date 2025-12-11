using eMeni.Application.Modules.Business.Business.Commands.Create;

namespace eMeni.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class BusinessController(ISender sender):ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateBusiness(CreateBusinessCommand command,CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return Created(string.Empty,new {id});
        }
    }
}
