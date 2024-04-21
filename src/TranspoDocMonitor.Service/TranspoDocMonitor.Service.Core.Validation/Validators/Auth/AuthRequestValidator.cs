using FluentValidation;
using TranspoDocMonitor.Service.Contracts.Shared.Auth;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Auth
{
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
