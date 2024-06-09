namespace TranspoDocMonitor.Service.Contracts.TransportDocument.GetDocument
{
    public record InfoTransportDocumentResponse(
        string Policyholder,
        string Beneficiary,
        int ContractNumberCCI,
        int NumberSeriesCCLI,
        Decimal Insurance,
        Decimal СoverageAmount,
        Guid VehicleId);

}

