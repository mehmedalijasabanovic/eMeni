using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eMeni.Infrastructure.Models.MenuCategoryEntity;

namespace eMeni.Application.Modules.Menu.MenuCategory.Commands.Update
{
    public sealed class UpdateMenuCategoryCommandValidator
      : AbstractValidator<UpdateMenuCategoryCommand>
    {
        public UpdateMenuCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .WithMessage("Category name is required")
                .MaximumLength(MenuCategoryConstraints.CategoryNameMaxLength)
                .WithMessage($"Category name max length is {MenuCategoryConstraints.CategoryNameMaxLength}");

            RuleFor(x => x.Description)
                .MaximumLength(MenuCategoryConstraints.DescriptionMaxLength)
                .WithMessage($"Category description max length is {MenuCategoryConstraints.DescriptionMaxLength}");

            RuleFor(x => x.OrderIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Order index must be zero or greater");
        }
    }
}
