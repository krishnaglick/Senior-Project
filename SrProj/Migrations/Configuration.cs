
using System.Collections.Generic;
using SrProj.Models;
using SrProj.Utility.Attribute;
using SrProj.Utility.ExtensionMethod;
using SrProj.Utility.Security;

namespace SrProj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SrProj.Models.Context.Database>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SrProj.Models.Context.Database context)
        {
            List<Role> roles = new List<Role>();
            foreach (RoleID role in Enum.GetValues(typeof (RoleID)))
            {
                roles.Add(new Role {
                    ID = (int)role,
                    RoleName = role.GetEnumAttribute<EnumDecorators.Name>().name,
                    RoleDescription = role.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }
            context.Roles.AddOrUpdate(roles.ToArray());

            Volunteer defaultUser = new Volunteer
            {
                HashedPassword = PasswordHasher.EncryptPassword("swordfish"),
                Username = "user",
                Roles = roles.Where(r => r.ID == (int)RoleID.Volunteer).ToList()
            };

            Volunteer adminUser = new Volunteer
            {
                HashedPassword = PasswordHasher.EncryptPassword("swordfish"),
                Username = "admin",
                Roles = roles
            };

            context.Volunteers.AddOrUpdate(defaultUser, adminUser);
        }
    }
}
