
using System;
using System.Collections.Generic;
using Models;

namespace DataAccess.TestData
{
    class ServiceGenerator : List<ServiceEligibility>
    {
        private Random _random;

        public ServiceGenerator(Random random = null)
        {
            _random = random ?? new Random();
        }
    }
}
