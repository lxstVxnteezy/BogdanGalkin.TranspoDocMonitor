using FluentValidation;
using TranspoDocMonitor.Service.Contracts.TransportDocument.Create;

namespace TranspoDocMonitor.Service.Core.Validation.Validators.Document
{
    public class CreateDocumentTransportRequestValidator : AbstractValidator<CreateTransportDocumentRequest>
    {
        public CreateDocumentTransportRequestValidator()
        {
           
        }
    }
}
 