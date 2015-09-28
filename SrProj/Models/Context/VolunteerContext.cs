
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SrProj.Models.Context
{
    public class VolunteerContext : ContextBase
    {
        public DbSet<Volunteer> Volunteers { get; set; }

        public override int SaveChanges()
        {
            foreach (var volunteer in Volunteers)
            {
                if (!string.IsNullOrEmpty(volunteer.Password))
                {
                    volunteer.SecurePassword();
                }
            }

            return base.SaveChanges();
        }
    }
}