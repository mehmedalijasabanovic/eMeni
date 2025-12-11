using eMeni.Infrastructure.Models;

namespace eMeni.Application.Modules.Business.Business.Commands.Create
{
    public class CreateBusinessCommandValidator : AbstractValidator<CreateBusinessCommand> 
    {
        public CreateBusinessCommandValidator() {
            RuleFor(x => x.BusinessName).NotEmpty().WithMessage("Business name is required field.").
                 MaximumLength(BusinessEntity.Constraint.BusinessNameMaxLength).
                 WithMessage($"Business Name max length is {BusinessEntity.Constraint.BusinessNameMaxLength}.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required field.").
              MaximumLength(BusinessEntity.Constraint.AddressMaxLength).
              WithMessage($"Address max length is {BusinessEntity.Constraint.AddressMaxLength}.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required field.").
            MaximumLength(BusinessEntity.Constraint.DescriptionMaxLength).
            WithMessage($"Description max length is {BusinessEntity.Constraint.DescriptionMaxLength}.");

        }
    }


}
