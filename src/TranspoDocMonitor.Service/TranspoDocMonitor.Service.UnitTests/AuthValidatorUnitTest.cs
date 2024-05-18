using FluentValidation.TestHelper;
using TranspoDocMonitor.Service.Contracts.Shared.Auth;
using TranspoDocMonitor.Service.Core.Validation.Validators.Auth;

namespace TranspoDocMonitor.Service.Tests.Validation
{
    public class AuthValidatorUnitTest
    {

        private readonly AuthValidatorUnit _validatorUnit;

        public AuthValidatorUnitTest()
        {
            _validatorUnit = new AuthValidatorUnit();
        }


        [Fact]
        public void ShouldHaveErrorWhenLoginIsEmpty()
        {
            //Arrange
            var model = new AuthRequest { Login = string.Empty, Password = "password123" };
            //Act
            var result = _validatorUnit.TestValidate(model);
            //Asserts
            result.ShouldHaveValidationErrorFor(x => x.Login);
        }

        [Fact]
        public void ShouldHaveErrorWhenPasswordIsEmpty()
        {
            //Arrange
            var model = new AuthRequest { Login = "user@example.com", Password = string.Empty };
            //Act
            var result = _validatorUnit.TestValidate(model);
            //Asserts
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void ShouldNotHaveErrorWhenFieldsArePopulated()
        {
            //Arrange
            var model = new AuthRequest { Login = "user@example.com", Password = "password123" };
            //Act
            var result = _validatorUnit.TestValidate(model);
            //Asserts
            result.ShouldNotHaveValidationErrorFor(x => x.Login);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }
    }
}
