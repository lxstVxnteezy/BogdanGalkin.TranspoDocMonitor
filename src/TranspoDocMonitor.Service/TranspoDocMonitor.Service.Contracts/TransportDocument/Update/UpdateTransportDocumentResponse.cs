﻿

namespace TranspoDocMonitor.Service.Contracts.TransportDocument.Update
{
    public record UpdateTransportDocumentResponse(
        string Policyholder,
        string Beneficiary,
        int ContractNumberCCI,
        int NumberSeriesCCLI,
        Decimal Insurance,
        Decimal СoverageAmount,
        Guid VehicleId);

}
