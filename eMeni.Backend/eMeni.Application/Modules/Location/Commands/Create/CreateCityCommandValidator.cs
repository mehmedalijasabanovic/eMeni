using eMeni.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Location.Commands.Create
{
    public sealed class CreateCityCommandValidator :AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required field.").
                MaximumLength(CityEntity.Constraint.NameMaxLength).
                WithMessage($"Name can be at most {CityEntity.Constraint.NameMaxLength} characters long.");
        }
    }
}
