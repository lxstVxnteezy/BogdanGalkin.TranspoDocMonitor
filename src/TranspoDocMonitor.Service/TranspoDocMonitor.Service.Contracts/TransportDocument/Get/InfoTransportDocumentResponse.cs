namespace TranspoDocMonitor.Service.Contracts.TransportDocument.GetDocument
{
    public record InfoTransportDocumentResponse(
        int DocumentNumber,
        DateTime DateOfIssue,
        DateTime ExpirationDateOfIssue,
        Guid UserVehicleId,
        Guid DictionaryDocumentTypeId);

}

