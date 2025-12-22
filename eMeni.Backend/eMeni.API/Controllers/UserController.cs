using eMeni.Application.Modules.Identity.Commands.Create;
using eMeni.Application.Modules.Identity.Commands.Delete;
using eMeni.Application.Modules.Identity.Commands.Update;
using eMeni.Application.Modules.Identity.Queries.GetById;
using eMeni.Application.Modules.Identity.Queries.List;

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
        [HttpGet]
        public async Task<PageResult<ListUserQueryDto>> List([FromQuery]ListUserQuery query,CancellationToken ct)
        {
            return await sender.Send(query, ct);
        }
        [HttpGet ("{id:int}")]
        public async Task<GetUserByIdQueryDto> GetById(int id,CancellationToken ct)
        {
            return await sender.Send(new GetUserByIdQuery { Id=id}, ct);
        }
    }
}
