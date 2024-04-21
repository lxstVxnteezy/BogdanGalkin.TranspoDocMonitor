

using FluentValidation;
using TranspoDocMonitor.Service.Contracts.Vehicle.Create;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Vehicle
{
    public class CreateVehicleValidator : AbstractValidator<CreateVehicleRequest>
    {
        public CreateVehicleValidator()
        {
            RuleFor(x=>x.Make).NotEmpty();
            RuleFor(X=>X.Model).NotEmpty();
            RuleFor(x=>x.Color).NotEmpty();
            RuleFor(x=>x.Year).NotEmpty();
            RuleFor(x => x.Year).Length(4).NotEmpty();
        }
    }
}
