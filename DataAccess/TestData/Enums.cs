
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

            foreach (EthnicityID ethnicity in Enum.GetValues(typeof(EthnicityID)))
            {
                context.Ethnicities.AddOrUpdate(new Ethnicity
                {
                    ID = (int)ethnicity,
                    Name = ethnicity.GetEnumAttribute<EnumDecorators.Name>().name,
                    Description = ethnicity.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }

            foreach (GenderID gender in Enum.GetValues(typeof(GenderID)))
            {
                context.Genders.AddOrUpdate(new Gender
                {
                    ID = (int)gender,
                    Name = gender.GetEnumAttribute<EnumDecorators.Name>().name,
                    Description = gender.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }

            foreach (MaritalStatusID marital in Enum.GetValues(typeof(MaritalStatusID)))
            {
                context.MaritalStatuses.AddOrUpdate(new MaritalStatus
                {
                    ID = (int)marital,
                    Name = marital.GetEnumAttribute<EnumDecorators.Name>().name,
                    Description = marital.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }

            foreach (RaceID race in Enum.GetValues(typeof(RaceID)))
            {
                context.Races.AddOrUpdate(new Race
                {
                    ID = (int)race,
                    Name = race.GetEnumAttribute<EnumDecorators.Name>().name,
                    Description = race.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }

            foreach (ResidenceStatusID residence in Enum.GetValues(typeof(ResidenceStatusID)))
            {
                context.ResidenceStatuses.AddOrUpdate(new ResidenceStatus
                {
                    ID = (int)residence,
                    Name = residence.GetEnumAttribute<EnumDecorators.Name>().name,
                    Description = residence.GetEnumAttribute<EnumDecorators.Description>().desc
                });
            }

            context.SaveChanges();
        }

        public static Role[] GetRoles(Database context)
        {
            return context.Roles.ToArray();
        }

        public static ServiceType[] GetServiceTypes(Database context)
        {
            return context.ServiceTypes.ToArray();
        }

        public static Ethnicity[] GetEthnicities(Database context)
        {
            return context.Ethnicities.ToArray();
        }

        public static Gender[] GetGenders(Database context)
        {
            return context.Genders.ToArray();
        }

        public static MaritalStatus[] GetMaritalStatuses(Database context)
        {
            return context.MaritalStatuses.ToArray();
        }

        public static Race[] GetRaces(Database context)
        {
            return context.Races.ToArray();
        }

        public static ResidenceStatus[] GetResidenceStatuses(Database context)
        {
            return context.ResidenceStatuses.ToArray();
        }

    }
}
