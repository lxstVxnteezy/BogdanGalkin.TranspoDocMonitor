using FluentValidation;

using TranspoDocMonitor.Service.Contracts.Pass.Create;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Passe
{
    public class CreatePassValidator : AbstractValidator<CreatePassRequest>
    {
        public CreatePassValidator()
        {
            RuleFor(x => x.From)
                .SetValidator(new FromValidator()).NotEmpty();
            RuleFor(x => x.PassNumber)
                .GreaterThan(0).WithMessage("PassNumber must be greater than 0.")
                .Must(passNumber => passNumber.ToString().Length >= 4)
                .WithMessage("PassNumber must have at least 4 digits.");
        }
    }
}
