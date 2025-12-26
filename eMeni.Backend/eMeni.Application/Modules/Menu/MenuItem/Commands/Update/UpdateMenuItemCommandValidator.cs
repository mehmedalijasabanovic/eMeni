using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eMeni.Infrastructure.Models.MenuItemEntity;

namespace eMeni.Application.Modules.Menu.MenuItem.Commands.Update
{
    public sealed class UpdateMenuItemCommandValidator
    : AbstractValidator<UpdateMenuItemCommand>
    {
        public UpdateMenuItemCommandValidator()
        {
            RuleFor(x => x.ItemName)
                .NotEmpty()
                .WithMessage("Item name is required")
                .MaximumLength(MenuItemConstraints.ItemNameMaxLength)
                .WithMessage($"Item name max length is {MenuItemConstraints.ItemNameMaxLength}");

            RuleFor(x => x.ItemDescription)
                .MaximumLength(MenuItemConstraints.ItemDescriptionMaxLength)
                .WithMessage($"Item description max length is {MenuItemConstraints.ItemDescriptionMaxLength}");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Item price is required")
                .MaximumLength(MenuItemConstraints.PriceMaxLength)
                .WithMessage($"Item price max length is {MenuItemConstraints.PriceMaxLength}");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(MenuItemConstraints.ImageUrlMaxLength)
                .WithMessage($"Image url max length is {MenuItemConstraints.ImageUrlMaxLength}");
        }
    }
}
