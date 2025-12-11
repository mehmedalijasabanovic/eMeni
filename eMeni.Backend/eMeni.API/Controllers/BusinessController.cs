using eMeni.Application.Modules.Business.Business.Commands.Create;
using eMeni.Application.Modules.Business.Business.Commands.Delete;

namespace eMeni.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class BusinessController(ISender sender) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateBusiness(CreateBusinessCommand command, CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return Created(string.Empty, new { id });
        }
        [HttpDelete ("{id:int}")]
        public async Task Delete(int id,CancellationToken ct)
        {
            await sender.Send(new DeleteBusinessCommand { Id=id }, ct);
        }
    }
}
