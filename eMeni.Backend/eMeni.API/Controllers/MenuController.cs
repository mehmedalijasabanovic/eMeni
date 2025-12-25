using eMeni.Application.Modules.Menu.Menu.Commands.Create;
using eMeni.Application.Modules.Menu.Menu.Commands.Delete;
using eMeni.Application.Modules.Menu.Menu.Queries.List;
using eMeni.Application.Modules.Menu.MenuCategory.Commands.Create;
using eMeni.Application.Modules.Menu.MenuCategory.Commands.Delete;
using eMeni.Application.Modules.Menu.MenuItem.Commands.Create;
using eMeni.Application.Modules.Menu.MenuItem.Commands.Delete;

namespace eMeni.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController(ISender sender):ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<int>> CreateMenu(CreateMenuCommand command,CancellationToken ct)
        {
            int id = await sender.Send(command, ct);
            return Created(string.Empty, new { id });
        }

        [HttpPost("{menuId}/categories")]
        public async Task<ActionResult<int>> CreateMenuCategory(int menuId, CreateMenuCategoryCommand command, CancellationToken ct)
        {
            command.MenuId= menuId;
            int id = await sender.Send(command, ct);
            return Created(string.Empty, new { id });
        }
        [HttpPost("categories/{categoryId}/items")]
        public async Task<ActionResult<int>> CreateMenuItem(int categoryId,CreateMenuItemCommand command, CancellationToken ct)
        {
            command.CategoryId= categoryId;
            int id = await sender.Send(command, ct);
            return Created(string.Empty, new { id });
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<PageResult<ListMenuQueryDto>> GetList([FromQuery]ListMenuQuery query,CancellationToken ct)
        {
            return await sender.Send(query, ct);
        }
        [HttpDelete("{menuId:int}")]
        public async Task DeleteMenu(int menuId, CancellationToken ct)
        {
            await sender.Send(new DeleteMenuCommand { Id = menuId }, ct);
        }

        [HttpDelete("categories/{categoryId:int}")]
        public async Task DeleteMenuCategory(int categoryId, CancellationToken ct)
        {
            await sender.Send(new DeleteMenuCategoryCommand { Id = categoryId }, ct);
        }

        [HttpDelete("items/{itemId:int}")]
        public async Task DeleteMenuItem(int itemId, CancellationToken ct)
        {
            await sender.Send(new DeleteMenuItemCommand { Id = itemId }, ct);
        }
    }
}
