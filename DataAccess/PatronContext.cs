using System.Data.Entity;
using Models;

namespace DataAccess
{
    public class PatronContext : ContextBase
    {
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
    }
}