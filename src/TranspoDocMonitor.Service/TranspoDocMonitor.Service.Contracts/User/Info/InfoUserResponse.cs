

namespace TranspoDocMonitor.Service.Contracts.User.Info
{
    public record InfoUserResponse(
        Guid Id,
        string Login,
        string FirstName,
        string? LastName,
        string? Surname,
        string? Email, 
        Guid RoleId,
        Domain.Library.Entities.Vehicle[] Vehicles
    );
}
