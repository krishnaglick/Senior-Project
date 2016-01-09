
using System.Data.Entity.Migrations;
using System.Linq;
using DataAccess.Contexts;
using Models;

namespace DataAccess.TestData
{
    public class TestVolunteers
    {
        public static void Seed(Database context)
        {
            context.Volunteers.AddOrUpdate(
                testUser,
                testAdmin
            );
        }

        public static readonly Volunteer testUser = new Volunteer
        {
            HashedPassword = Volunteer.hasher.HashPassword("swordfish"),
            Username = "user"
        };

        public static readonly Volunteer testAdmin = new Volunteer
        {
            HashedPassword = Volunteer.hasher.HashPassword("swordfish"),
            Username = "admin"
        };

        public static Volunteer[] GetTestVolunteers(Database context)
        {
            return context.Volunteers.ToArray()
                .Where(
                    v => v.Username == testAdmin.Username ||
                    v.Username == testUser.Username
                ).ToArray();
        }
    }
}
