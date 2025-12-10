namespace eMeni.Application.Modules.Business.Business.Commands.Create
{
    public class CreateBusinessCommandHandler(IAppDbContext db):IRequestHandler<CreateBusinessCommand,int>
    {
        public async Task<int> Handle(CreateBusinessCommand command, CancellationToken ct)
        {

            return 0;
        }
    }


}
