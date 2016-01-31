
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contexts;
using Models;

namespace DataAccess.TestData
{
    public class Visits
    {
        public static Random Random = new Random();
        public static Visit CreateRandomVisit(Database context)
        {
            var randomServiceTypeID = Random.Next(1, context.ServiceTypes.Count());
            var randomPatronID = Random.Next(1, context.Patrons.Count());
            return new Visit
            {
                Patron = context.Patrons.FirstOrDefault(p => p.ID == randomPatronID),
                Service = context.ServiceTypes.FirstOrDefault(st => st.ID == randomServiceTypeID),
                CreateVolunteer = context.Volunteers.FirstOrDefault(v => v.Username == TestVolunteers.testAdmin.Username)
            };
        }

        public static Visit[] GetRandomVisits(int number, Database context)
        {
            var patrons = new List<Visit>();
            for (int i = 0; i < number; i++)
            {
                patrons.Add(CreateRandomVisit(context));
            }

            return patrons.ToArray();
        }

        public static void Seed(Database context, int number = 1)
        {
            context.Visits.AddRange(GetRandomVisits(number, context));
            context.SaveChanges();
        }


    }
}
