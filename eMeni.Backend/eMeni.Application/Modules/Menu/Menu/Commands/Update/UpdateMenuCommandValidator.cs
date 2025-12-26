using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eMeni.Infrastructure.Models.MenuEntity;

namespace eMeni.Application.Modules.Menu.Menu.Commands.Update
{
    public sealed class UpdateMenuCommandValidator
    : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidator()
        {
            RuleFor(x => x.MenuTitle)
                .NotEmpty()
                .WithMessage("Menu title is required")
                .MaximumLength(MenuConstraints.MenuTitleMaxLength)
                .WithMessage($"Menu title max length is {MenuConstraints.MenuTitleMaxLength}");

            RuleFor(x => x.MenuDescription)
                .MaximumLength(MenuConstraints.MenuDescriptionMaxLength)
                .WithMessage($"Menu description max length is {MenuConstraints.MenuDescriptionMaxLength}");
        }
    }
}
