namespace TranspoDocMonitor.Service.Contracts.TransportDocument.Create
{
    public record CreateTransportDocumentRequest(
        string Policyholder,
        string Beneficiary,
        int ContractNumberCCI,
        int NumberSeriesCCLI,
        Decimal Insurance,
        Decimal СoverageAmount);

}
