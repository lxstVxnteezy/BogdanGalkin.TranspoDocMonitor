namespace TranspoDocMonitor.Service.Contracts.User.Create
{
    public record CreateUserRequest(
        string Login,
        string Password,
        string FirstName,
        string? LastName,
        string? Surname,
        string Email,
        Guid RoleId
 );

}
