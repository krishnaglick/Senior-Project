
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DataAccess.Contexts;
using Models;
using Utility.Attribute;
using Utility.Enum;
using Utility.ExtensionMethod;
using ServiceType = Models.ServiceType;

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
            List<Role> roles = new List<Role>();
            foreach (RoleID role in Enum.GetValues(typeof (RoleID)))
            {
                roles.Add(new Role {
                    ID = role,
                    RoleName = role.GetEnumAttribute<EnumDecorators.Name>().name,
                    RoleDescription = role.GetEnumAttribute<EnumDecorators.Description>().desc,
                    Volunteers = new List<Volunteer>()
                });
            }

            Volunteer defaultUser = new Volunteer
            {
                HashedPassword = Volunteer.hasher.HashPassword("swordfish"),
                Username = "user",
                Roles = roles.Where(r => r.ID == RoleID.Volunteer).ToArray()
            };

            Volunteer adminUser = new Volunteer
            {
                HashedPassword = Volunteer.hasher.HashPassword("swordfish"),
                Username = "admin",
                Roles = roles.ToArray()
            };

            roles.First(r => r.ID == RoleID.Volunteer).Volunteers.Add(defaultUser);
            roles.First(r => r.ID == RoleID.Volunteer).Volunteers.Add(adminUser);
            roles.First(r => r.ID == RoleID.Admin).Volunteers.Add(adminUser);

            context.Roles.AddOrUpdate(roles.ToArray());

            List<ServiceType> services = new List<ServiceType>();
            foreach (ServiceTypeID service in Enum.GetValues(typeof(ServiceTypeID)))
            {
                services.Add(new ServiceType
                {
                    ID = (int)service,
                    ServiceName = service.GetEnumAttribute<EnumDecorators.Name>().name,
                    ServiceDescription = service.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }

            context.ServiceTypes.AddOrUpdate(services.ToArray());

            context.SaveChanges();
        }
    }
}
