using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Menu.MenuItem.Commands.Create
{
    public sealed class CreateMenuItemCommandValidator : AbstractValidator<CreateMenuItemCommand>
    {
        public CreateMenuItemCommandValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0);
            RuleFor(x => x.ItemName).NotEmpty().MaximumLength(MenuItemEntity.MenuItemConstraints.ItemDescriptionMaxLength);
            RuleFor(x => x.Price).NotEmpty().MaximumLength(MenuItemEntity.MenuItemConstraints.ItemDescriptionMaxLength);
        }
    }
}
