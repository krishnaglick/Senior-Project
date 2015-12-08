
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            foreach (RoleID role in Enum.GetValues(typeof(RoleID)))
            {
                roles.Add(new Role
                {
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
                    Role = roles.First(r => r.ID == (int)RoleID.Volunteer),
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

            var patrons = new List<Patron>();
            var ethnicity = new Ethnicity()
            {
                Name = "Asian",
                Description = "Asian"
            };
            var gender = new Gender()
            {
                Name = "Male",
                Description = "Male"
            };

            var residence = new ResidenceStatus()
            {
                Name = "US Citizen",
                Description = "US Citizen"
            };

            var marital = new MaritalStatus()
            {
                Name = "Married",
                Description = "Married"
            };

            for (var i = 0; i < 20; i++)
            {
             
               
                patrons.Add(new Patron
                {

                    FirstName = "TestPatronFname" + i,
                    LastName = "TestPatronLname" + i,
                    //FullName = 
                    NumberInHousehold = 4,
                    Banned = false,
                    Marital = marital,
                    Residence = residence,
                    Gender = gender,
                    Ethnicity = ethnicity,
                    DateOfBirth = new DateTime(1970, 12, 5),
                    PhoneNumbers = new List<PhoneNumber>
                    {
                        new PhoneNumber
                        {
                            ContactNumber = "904-245-6789"
                        },
                        new PhoneNumber
                        {
                            ContactNumber = "904-747-6789"
                        }
                    },
                    Addresses = new List<Address>
                    {
                        new Address
                        {
                            City = "Jacksonville",
                            County = "Duval",
                            State = "FL",
                            StreetAddress = "123 Main Street",
                            Zip = "32221"
                        }

                    },
                    EmergencyContacts = new List<EmergencyContact>
                    {
                        new EmergencyContact
                        {
                            FirstName = "EmergFname"+i,
                            LastName = "EmergLname"+i,
                            PhoneNumbers = new List<PhoneNumber>
                            {
                           
                                new PhoneNumber
                                {
                                    ContactNumber = "904-781-2222"
                                }
                            },
                        },
                    }
                });
            };

            var serviceTypePantry = new ServiceType
            {
                ServiceName = "The Lord's Pantry",
                ServiceDescription = "This service is for patrons who come for food."
            };

            var serviceEligiblities = new List<ServiceEligibility>();

            foreach (var patron in patrons)
            {
                serviceEligiblities.Add(new ServiceEligibility
                {
                    Patron = patron,
                    ServiceType = serviceTypePantry
                });
            }

            context.Patrons.AddRange(patrons);
            context.ServiceEligibilities.AddRange(serviceEligiblities);
            context.SaveChanges();
        }
    }
}
