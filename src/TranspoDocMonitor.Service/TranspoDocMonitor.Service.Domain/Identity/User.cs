using TranspoDocMonitor.Service.Domain.Base;
using TranspoDocMonitor.Service.Domain.Library.StagingTables;

namespace TranspoDocMonitor.Service.Domain.Identity
{
    public class User : BaseEntity
    {
        public string Login { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string? Surname { get; set; }

        public string Hash { get; set; } = null!;

        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }


        public virtual ICollection<UserTransport> UserTransports { get; set; }

    }
}
