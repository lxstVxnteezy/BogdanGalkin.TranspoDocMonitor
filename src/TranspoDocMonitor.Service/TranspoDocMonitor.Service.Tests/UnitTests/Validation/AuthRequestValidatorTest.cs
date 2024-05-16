using FluentValidation.TestHelper;
using TranspoDocMonitor.Service.Contracts.Shared.Auth;
using TranspoDocMonitor.Service.Core.Validation.Validators.Auth;

namespace TranspoDocMonitor.Service.Tests.Validation
{
    public class AuthRequestValidatorTest
    {

        private readonly AuthRequestValidator _validator;

        public AuthRequestValidatorTest()
        {
            _validator = new AuthRequestValidator();
        }


        [Fact]
        public void ShouldHaveErrorWhenLoginIsEmpty()
        {
            var model = new AuthRequest { Login = string.Empty, Password = "password123" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Login);
        }

        [Fact]
        public void ShouldHaveErrorWhenPasswordIsEmpty()
        {
            var model = new AuthRequest { Login = "user@example.com", Password = string.Empty };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenFieldsArePopulated()
        {
            var model = new AuthRequest { Login = "user@example.com", Password = "password123" };
            var result = _validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.Login);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }
    }
}
