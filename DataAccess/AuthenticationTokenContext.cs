
using System.Data.Entity;
using Models;

namespace DataAccess
{
    public class AuthenticationTokenContext : ContextBase
    {
        public DbSet<AuthenticationToken> AuthenticationTokens { get; set; }
    }
}