
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
                    ID = (int)role,
                    RoleName = role.GetEnumAttribute<EnumDecorators.Name>().name,
                    RoleDescription = role.GetEnumAttribute<EnumDecorators.Description>().desc,
                    Volunteers = new List<RoleVolunteer>()
                });
            }

            Volunteer defaultUser = new Volunteer
            {
                HashedPassword = Volunteer.hasher.HashPassword("swordfish"),
                Username = "user",
                Roles = new List<RoleVolunteer>()
            };

            Volunteer adminUser = new Volunteer
            {
                HashedPassword = Volunteer.hasher.HashPassword("swordfish"),
                Username = "admin",
                Roles = new List<RoleVolunteer>()
            };

            List<RoleVolunteer> roleVolunteers = new List<RoleVolunteer>();

            roleVolunteers.Add(
                new RoleVolunteer
                {
                    Role = roles.First(r => r.ID == (int) RoleID.Volunteer),
                    Volunteer = defaultUser
                }
            );
            roleVolunteers.Add(
                new RoleVolunteer
                {
                    Role = roles.First(r => r.ID == (int)RoleID.Volunteer),
                    Volunteer = adminUser
                }
            );
            roleVolunteers.Add(
                new RoleVolunteer
                {
                    Role = roles.First(r => r.ID == (int)RoleID.Admin),
                    Volunteer = adminUser
                }
            );

            context.Roles.AddOrUpdate(roles.ToArray());
            context.RoleVolunteers.AddOrUpdate(roleVolunteers.ToArray());

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
