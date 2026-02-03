using eMeni.Application.Modules.Business.Business.Commands.Create;
using eMeni.Application.Modules.Business.Business.Commands.Delete;
using eMeni.Application.Modules.Business.Business.Commands.Update;
using eMeni.Application.Modules.Business.Business.Commands.UpdatePromotionRank;
using eMeni.Application.Modules.Business.Business.Queries.GetByUserId;
using eMeni.Application.Modules.Business.Business.Queries.List;


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

        [HttpPut ("{id:int}")]
        public async Task Update(int id,UpdateBusinessCommand command,CancellationToken ct)
        {
            command.Id = id;
            await sender.Send(command, ct);
        }
        [HttpPut("{id:int}/promotion-rank")]
        public async Task UpdateBusinessPromotionRank(int id, UpdateBusinessPromotionRankCommand command, CancellationToken ct)
        {
            command.Id = id;
            await sender.Send(command, ct);
        }

        [HttpGet]
        public async Task<PageResult<ListBusinessQueryDto>> List([FromQuery]ListBusinessQuery query,CancellationToken ct)
        {
            return await sender.Send(query,ct);
        }
        [HttpGet ("my-businesses")]
        public async Task<List<GetBusinessByUserIdQueryDto>> GetById(CancellationToken ct)
        {
        
            return await sender.Send(new GetBusinessByUserIdQuery(),ct);
        }
    }
}
