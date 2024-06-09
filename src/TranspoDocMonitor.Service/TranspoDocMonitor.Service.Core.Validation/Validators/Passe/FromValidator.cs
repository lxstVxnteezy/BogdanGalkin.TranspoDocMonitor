using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranspoDocMonitor.Service.Domain.EnumTypes;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Passe
{
    public class FromValidator : AbstractValidator<From>
    {
        public FromValidator()
        {
            RuleFor(x => x)
                .IsInEnum()
                .WithMessage("Invalid From value.");
        }
    }
}
