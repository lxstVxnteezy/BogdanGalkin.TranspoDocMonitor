

namespace TranspoDocMonitor.Service.Contracts.TransportDocument.Update
{
    public record UpdateTransportDocumentRequest(
        string Policyholder,
        string Beneficiary,
        int ContractNumberCCI,
        int NumberSeriesCCLI,
        Decimal Insurance,
        Decimal СoverageAmount);

}
