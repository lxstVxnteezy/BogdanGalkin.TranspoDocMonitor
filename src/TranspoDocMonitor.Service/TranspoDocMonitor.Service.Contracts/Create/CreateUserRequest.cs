namespace TranspoDocMonitor.Service.Contracts.Create
{
    public record CreateUserRequest(
        string Login,
        string Password,
        string FirstName,
        string? LastName,
        string? Surname,
        Guid RoleId
 );

}
