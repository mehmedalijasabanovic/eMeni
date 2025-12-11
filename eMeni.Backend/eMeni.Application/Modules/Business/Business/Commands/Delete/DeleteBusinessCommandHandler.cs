namespace eMeni.Application.Modules.Business.Business.Commands.Delete
{
    public sealed class DeleteBusinessCommandHandler(IAppDbContext db,IAppCurrentUser user) : IRequestHandler<DeleteBusinessCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteBusinessCommand command,CancellationToken ct)
        {

            return Unit.Value;
        }
    }
}
