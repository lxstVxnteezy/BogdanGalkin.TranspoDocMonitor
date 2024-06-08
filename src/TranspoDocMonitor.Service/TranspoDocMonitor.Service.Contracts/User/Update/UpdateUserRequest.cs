

namespace TranspoDocMonitor.Service.Contracts.User.Update
{
    public record UpdateUserRequest(
        string FirstName,
        string? LastName,
        string? Surname,
        string? Email,
        Guid RoleId
    );
}
