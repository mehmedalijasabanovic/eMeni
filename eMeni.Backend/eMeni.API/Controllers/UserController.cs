using eMeni.Application.Modules.Identity.Commands.Create;
using eMeni.Application.Modules.Identity.Commands.Delete;
using eMeni.Application.Modules.Identity.Commands.Update;

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
        [HttpDelete ("{id:int}")]
        public async Task Delete(int id,CancellationToken ct)
        {
            await sender.Send(new DeleteUserCommand { Id=id}, ct);
        }
        [HttpPut ("{id:int}")]
        public async Task Update(int id,UpdateUserCommand command,CancellationToken ct)
        {
            command.Id=id;
            await sender.Send(command, ct);
        }
    }
}
