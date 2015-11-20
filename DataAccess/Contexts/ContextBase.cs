using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Models;

namespace DataAccess.Contexts
{
    public class ContextBase : DbContext
    {
        public ContextBase() : base("homeNetwork") { }

        public ContextBase(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    //TODO: Not this.
    public class Database : ContextBase
    {
        public Database() { }
        public Database(string connectionString) : base(connectionString) { }

        public DbSet<Patron> Patrons { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<AuthenticationToken> AuthenticationTokens { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ResidenceStatus> ResidenceStatuses { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Race> Races { get; set; }

    }
}