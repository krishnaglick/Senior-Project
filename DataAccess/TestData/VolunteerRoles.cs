
using System.Data.Entity.Migrations;
using DataAccess.Contexts;
using Models;
using Utility.Enum;

namespace DataAccess.TestData
{
    public class VolunteerRoles
    {
        public static void Seed(Database context)
        {
            context.RoleVolunteers.AddOrUpdate(
                new RoleVolunteer
                {
                    RoleID = (int) RoleID.Volunteer,
                    VolunteerUsername = "user"
                }
            );

            context.RoleVolunteers.AddOrUpdate(
                new RoleVolunteer
                {
                    RoleID = (int)RoleID.Volunteer,
                    VolunteerUsername = "admin"
                }
            );

            context.RoleVolunteers.AddOrUpdate(
                new RoleVolunteer
                {
                    RoleID = (int)RoleID.Admin,
                    VolunteerUsername = "admin"
                }
            );

            context.SaveChanges();
        }
    }
}
