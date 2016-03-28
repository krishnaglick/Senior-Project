
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Contexts;
using Models;

namespace DataAccess.TestData
{
    class ServiceGenerator : List<ServiceEligibility>
    {
        public ServiceGenerator(Database database, Random random)
        {
            var serviceTypes = database.ServiceTypes.Select(st => st).ToArray();
            for (int i = 0; i < random.Next(1, 8); i++)
            {
                this.Add(
                    new ServiceEligibility
                    {
                        ServiceType = serviceTypes[random.Next(0, serviceTypes.Length)]
                    }
                );
            }
        }
    }
}
