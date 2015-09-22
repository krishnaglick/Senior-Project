
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SrProj.Models.Context
{
    public class PatronContext : ContextBase
    {
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}