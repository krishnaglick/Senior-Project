using System.Data.Entity;
using Models;

namespace DataAccess
{
    public class RoleContext : ContextBase
    {
        public DbSet<Role> Roles { get; set; }
    }
}