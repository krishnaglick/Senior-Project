
using Models;
using System;
using System.Collections.Generic;
using DataAccess.Contexts;

namespace DataAccess.TestData
{
    class Patrons
    {
        public static Random Random = new Random();
        public static Patron GetRandomPatron(Database context)
        {
            var namesSize = Names.Length;
            var maritalStatuses = Enums.GetMaritalStatuses(context);
            var residenceStatuses = Enums.GetResidenceStatuses(context);
            var genderStatuses = Enums.GetGenders(context);
            var ethnicityStatuses = Enums.GetEthnicities(context);
            //TODO: Race id
                
            return new Patron
            {
                FirstName = Names[Random.Next(namesSize)],
                LastName = Names[Random.Next(namesSize)],
                DateOfBirth = new DateTime(Random.Next(1970, 2015), Random.Next(1, 12), Random.Next(1, 28)),
                Marital = maritalStatuses[Random.Next(maritalStatuses.Length)],
                Residence = residenceStatuses[Random.Next(residenceStatuses.Length)],
                Gender = genderStatuses[Random.Next(genderStatuses.Length)],
                Ethnicity = ethnicityStatuses[Random.Next(ethnicityStatuses.Length)],
                Addresses = new AddressGenerator(Random.Next(5), Random).ToArray(),
                NumberInHousehold = (short) Random.Next(1, 10)
            };
        }

        public static Patron[] GetRandomPatrons(int number, Database context)
        {
            var patrons = new List<Patron>();
            for (int i = 0; i < number; i++)
            {
                patrons.Add(GetRandomPatron(context));
            }

            return patrons.ToArray();
        }

        public static void Seed(Database context, int number = 1)
        {
            context.Patrons.AddRange(GetRandomPatrons(number, context));
            context.SaveChanges();
        }

        public static string[] Names = new string[] { "Nicolette", "Bernarda", "Tandra", "Micha", "Marylyn", "Shawanda", "Kraig", "Taisha", "Melda", "Venice", "Ema", "Olympia", "Annett", "Celine", "Hipolito", "Breanna", "Patrica", "Tiara", "Fatimah", "Takako", "Regena", "Lakita", "Rosalie", "Karyl", "Markita", "Joellen", "Mickey", "Quentin", "Ute", "Peggie" };
    }
}
