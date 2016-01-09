
using System.Data.Entity.Migrations;
using System.Linq;
using DataAccess.Contexts;
using Models;
using Utility.Enum;

namespace DataAccess.TestData
{
    public class VolunteerRoles
    {
        public static void Seed(Database context)
        {
            var Roles = Enums.GetRoles(context);
            var Volunteers = TestData.TestVolunteers.GetTestVolunteers(context);

            context.RoleVolunteers.AddOrUpdate(
                new RoleVolunteer
                {
                    Role = Roles.SingleOrDefault(r => r.ID == (int) RoleID.Volunteer),
                    Volunteer = Volunteers.SingleOrDefault(v => v.Username == "user")
                }
            );

            context.RoleVolunteers.AddOrUpdate(
                new RoleVolunteer
                {
                    Role = Roles.SingleOrDefault(r => r.ID == (int)RoleID.Volunteer),
                    Volunteer = Volunteers.SingleOrDefault(v => v.Username == "admin")
                }
            );

            context.RoleVolunteers.AddOrUpdate(
                new RoleVolunteer
                {
                    Role = Roles.SingleOrDefault(r => r.ID == (int)RoleID.Admin),
                    Volunteer = Volunteers.SingleOrDefault(v => v.Username == "admin")
                }
            );
        }
    }
}
