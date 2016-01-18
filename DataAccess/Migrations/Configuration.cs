
using System;
using System.Data.Entity.Migrations;
using DataAccess.Contexts;
using DataAccess.TestData;

namespace DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Database>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Database context)
        {
            Enums.Seed(context);
            TestVolunteers.Seed(context);
            VolunteerRoles.Seed(context);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
