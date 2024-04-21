
using FluentValidation;
using TranspoDocMonitor.Service.Contracts.User.Create;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.User
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Login).NotEmpty();
            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
