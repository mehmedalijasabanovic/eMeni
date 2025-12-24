namespace eMeni.Application.Modules.Menu.Menu.Commands.Create
{
    public sealed class CreateMenuCommandHandler(IAppDbContext db,IAuthorizationHelper auth) : IRequestHandler<CreateMenuCommand, int>
    {
        public async Task<int> Handle(CreateMenuCommand command,CancellationToken ct)
        {

            return 0;
        }
    }
}
