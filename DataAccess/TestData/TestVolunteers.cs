
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Models;
using Utility.Enum;
using Database = DataAccess.Contexts.Database;

namespace DataAccess.TestData
{
    public class TestVolunteers
    {
        public static void Seed(Database context)
        {
            var tempUser = context.Volunteers.FirstOrDefault(v => v.Username == testUser.Username);
            var tempAdmin = context.Volunteers.FirstOrDefault(v => v.Username == testAdmin.Username);
            if (tempUser != null && tempAdmin != null) return;

            testUser.ServiceTypes = new List<ServiceType>
            {
                context.ServiceTypes.First(st => st.ID == (int) ServiceTypeID.Dental)
            };
            testAdmin.ServiceTypes = new List<ServiceType>();
            context.ServiceTypes.ToList().ForEach(st => testAdmin.ServiceTypes.Add(st));
            context.Volunteers.AddOrUpdate(
                testUser,
                testAdmin
            );

            context.SaveChanges();
        }

        public static Volunteer testUser = new Volunteer
        {
            HashedPassword = Volunteer.hasher.HashPassword("swordfish"),
            Username = "user"
        };

        public static Volunteer testAdmin = new Volunteer
        {
            HashedPassword = Volunteer.hasher.HashPassword("swordfish"),
            Username = "admin",
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
