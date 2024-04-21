using FluentValidation;
using TranspoDocMonitor.Service.Contracts.DocumentType;


namespace TranspoDocMonitor.Service.Core.Validation.Validators.Document
{
    public class CreateDocumentTypeRequestValidator : AbstractValidator<CreateDocumentTypeRequest>
    {
        public CreateDocumentTypeRequestValidator()
        {
            RuleFor(x => x.DocumentName).NotEmpty();
        }
    }
}

