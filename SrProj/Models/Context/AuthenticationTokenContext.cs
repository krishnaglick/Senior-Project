
using System.Data.Entity;

namespace SrProj.Models.Context
{
    public class AuthenticationTokenContext : ContextBase
    {
        public DbSet<AuthenticationToken> AuthenticationTokens { get; set; }
    }
}