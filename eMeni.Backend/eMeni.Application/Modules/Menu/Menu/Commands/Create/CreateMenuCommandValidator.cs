using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Menu.Menu.Commands.Create
{
    public sealed class CreateMenuCommandValidator:AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(x => x.BusinessId).GreaterThan(0);
            RuleFor(x => x.MenuTitle).NotEmpty().MaximumLength(MenuEntity.MenuConstraints.MenuTitleMaxLength);
            RuleFor(x => x.MenuDescription).MaximumLength(MenuEntity.MenuConstraints.MenuDescriptionMaxLength);
        }
    }
}
