using FluentValidation;
using TranspoDocMonitor.Service.Contracts.Shared.Auth;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Auth
{
    public class AuthValidatorUnit : AbstractValidator<AuthRequest>
    {
        public AuthValidatorUnit()
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
