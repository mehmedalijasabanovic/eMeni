using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Business.BusinessCategory.Commands.Create
{
    public class CreateBusinessesCategoryCommandValidator:AbstractValidator<CreateBusinessesCategoryCommand>
    {
        public CreateBusinessesCategoryCommandValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required field.")
                .MaximumLength(BusinessesCategoryEntity.Constraint.NameMaxLenght).WithMessage($"Name can be at most {BusinessesCategoryEntity.Constraint.NameMaxLenght} characters long.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required field.")
               .MaximumLength(BusinessesCategoryEntity.Constraint.NameMaxLenght).WithMessage($"Description can be at most {400} characters long.");
        }
    }
}
