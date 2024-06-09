using FluentValidation;

using TranspoDocMonitor.Service.Contracts.Vehicle.Update;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Vehicle
{
    public class UpdateVehicleValidator : AbstractValidator<UpdateVehicleRequest>
    {

        public UpdateVehicleValidator()
        {
            RuleFor(x => x.Make).NotEmpty();
            RuleFor(x => x.VehicleIdentificationNumber)
                .NotEmpty()
                .WithMessage("VIN number is required.")
                .Matches(@"^[A-HJ-NPR-Z0-9]{17}$")
                .WithMessage("Invalid VIN format.");
            RuleFor(x => x.EngineCapacity).NotEmpty();
            RuleFor(X => X.Model).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
            RuleFor(x => x.Year).NotEmpty();
            RuleFor(x => x.Year).Length(4).NotEmpty();
            RuleFor(x => x.DiagnosticCardNumber)
                .Must(HasValidNumberOfDigits)
                .WithMessage("DiagnosticCardNumber must be between 15 and 21 digits long.");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price cannot be negative.");
            RuleFor(x => x.Price)
                .Must(HasMinimumFourCharacters)
                .WithMessage("Price must have at least 4 characters including digits and decimal point.");
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(1000)
                .WithMessage("Price must be at least 1000.");

            RuleFor(x => x.RegistrationNumber)
                .Matches(@"^[A-Z]{2}\s\d{7}$")
                .WithMessage("Invalid registration number format. Expected format: AA 0085310");
        }
        private bool HasValidNumberOfDigits(long number)
        {
            string numberAsString = number.ToString();
            int length = numberAsString.Length;
            return length >= 15 && length <= 21;
        }
        private bool HasMinimumFourCharacters(decimal price)
        {
            string priceStr = price.ToString("G");


            int characterCount = priceStr.Length;

            return characterCount >= 4;
        }
    }
}
