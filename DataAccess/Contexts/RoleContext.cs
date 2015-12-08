using System.Data.Entity;
using Models;

namespace DataAccess.Contexts
{
    public class RoleContext : ContextBase
    {
        public DbSet<Role> Roles { get; set; }
    }
}