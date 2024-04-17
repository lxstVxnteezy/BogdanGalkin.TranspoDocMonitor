

namespace TranspoDocMonitor.Service.Contracts.TransportDocument.Create
{
    public record CreateTransportDocumentRequest(int DocumentNumber, DateTime DateOfIssue, DateTime ExpirationDateOfIssue, Guid DictionaryDocumentTypeId,Guid VehicleId);

}
