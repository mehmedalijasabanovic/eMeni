using eMeni.Application.Modules.Identity.Commands.Create;
using eMeni.Application.Modules.Identity.Commands.Delete;

namespace eMeni.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public sealed class UserController(ISender sender):ControllerBase
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Create(CreateUserCommand cmd,CancellationToken ct)
        {
            int id = await sender.Send(cmd,ct);
            return Created(string.Empty, new { id });
        }
        [HttpDelete]
        public async Task Delete(DeleteUserCommand cmd,CancellationToken ct)
        {
            await sender.Send(new DeleteUserCommand { Id=cmd.Id}, ct);
        }
    }
}
