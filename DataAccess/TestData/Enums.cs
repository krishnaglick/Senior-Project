
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DataAccess.Contexts;
using Models;
using Utility.Attribute;
using Utility.Enum;
using Utility.ExtensionMethod;

namespace DataAccess.TestData
{
    public class Enums
    {
        public static void Seed(Database context)
        {
            foreach (RoleID role in Enum.GetValues(typeof (RoleID)))
            {
                context.Roles.AddOrUpdate(
                    new Role
                    {
                        ID = (int)role,
                        RoleName = role.GetEnumAttribute<EnumDecorators.Name>().name,
                        RoleDescription = role.GetEnumAttribute<EnumDecorators.Description>().desc
                    }
                );
            }

            foreach (ServiceTypeID service in Enum.GetValues(typeof(ServiceTypeID)))
            {
                context.ServiceTypes.AddOrUpdate(new ServiceType
                {
                    ID = (int)service,
                    ServiceName = service.GetEnumAttribute<EnumDecorators.Name>().name,
                    ServiceDescription = service.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }
        }

        public static Role[] GetRoles(Database context)
        {
            return context.Roles.ToArray();
        }
    }
}
