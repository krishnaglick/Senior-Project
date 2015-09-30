using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SrProj.Models.Context
{
    public class RoleContext : ContextBase
    {
        public DbSet<Role> Roles { get; set; }
    }
}