using TranspoDocMonitor.Service.Domain.Base;


namespace TranspoDocMonitor.Service.Domain.Identity
{
    public class Role : BaseEntity
    {
        public Role()
        {
            Users = new List<User>();
        }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
