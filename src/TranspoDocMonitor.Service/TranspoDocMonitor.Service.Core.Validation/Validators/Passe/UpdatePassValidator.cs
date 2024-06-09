using FluentValidation;

using TranspoDocMonitor.Service.Contracts.Pass.Update;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Passe
{
    internal class UpdatePassValidator : AbstractValidator<UpdatePassRequest>
    {
        public UpdatePassValidator()
        {
            RuleFor(x => x.From)
                .SetValidator(new FromValidator()).NotEmpty();
        }
    }
}
