using eMeni.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Menu.MenuCategory.Commands.Create
{
    public sealed class CreateMenuCategoryCommandValidator:AbstractValidator<CreateMenuCategoryCommand>
    {
        public CreateMenuCategoryCommandValidator()
        {
            RuleFor(x => x.MenuId).GreaterThan(0);
            RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(MenuCategoryEntity.MenuCategoryConstraints.CategoryNameMaxLength);
            RuleFor(x => x.OrderIndex).GreaterThanOrEqualTo(0);
        }
    }
}
