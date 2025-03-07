﻿using FluentValidation;
using TranspoDocMonitor.Service.Contracts.TransportDocument.Create;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Document
{
    public class CreateDocumentTransportRequestValidator : AbstractValidator<CreateTransportDocumentRequest>
    {
        public CreateDocumentTransportRequestValidator()
        {
            RuleFor(x => x.Policyholder)
                .NotEmpty().WithMessage("Policyholder is required.");

            RuleFor(x => x.Beneficiary)
                .NotEmpty().WithMessage("Beneficiary is required.");

            RuleFor(x => x.ContractNumberCCI)
                .GreaterThan(0).WithMessage("ContractNumberCCI must be greater than 0.");

            RuleFor(x => x.NumberSeriesCCLI)
                .GreaterThan(0).WithMessage("NumberSeriesCCLI must be greater than 0.");

            RuleFor(x => x.Insurance)
                .GreaterThanOrEqualTo(0).WithMessage("Insurance must be non-negative.");

            RuleFor(x => x.СoverageAmount)
                .GreaterThan(0).WithMessage("CoverageAmount must be greater than 0.");
        }
    }
}
 