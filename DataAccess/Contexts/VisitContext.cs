using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Contexts
{
    public class VisitContext : ContextBase
    {
        public DbSet<Visit> Visits { get; set; }
    }
}
