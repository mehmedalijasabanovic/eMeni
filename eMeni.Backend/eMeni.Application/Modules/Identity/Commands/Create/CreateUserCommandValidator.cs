using eMeni.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMeni.Application.Modules.Identity.Commands.Create
{
    public sealed class CreateUserCommandValidator:AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() {
            RuleFor(x => x.Email).NotEmpty().WithMessage("User email is required")
                .MaximumLength(eMeniUserEntity.UserConstraints.EmailMaxLength).
                WithMessage($"User email max lenght is {eMeniUserEntity.UserConstraints.EmailMaxLength}");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("User name is required")
            .MaximumLength(eMeniUserEntity.UserConstraints.FullNameMaxLength).
            WithMessage($"User full name max lenght is {eMeniUserEntity.UserConstraints.FullNameMaxLength}");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("User phone number is required")
            .MaximumLength(eMeniUserEntity.UserConstraints.PhoneMaxLength).
            WithMessage($"User phone number max lenght is {eMeniUserEntity.UserConstraints.PhoneMaxLength}");
        }
    }
}
