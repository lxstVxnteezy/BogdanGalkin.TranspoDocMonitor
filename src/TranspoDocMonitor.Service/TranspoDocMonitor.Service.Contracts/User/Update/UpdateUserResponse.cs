using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranspoDocMonitor.Service.Contracts.User.Update
{
    public record UpdateUserResponse(
        string Login,
        string FirstName,
        string? LastName,
        string? Surname,
        string? Email,
        Guid RoleId
    );
}
